locals {
  timestamp_parts    = regex("^(?P<year>\\d+)(?P<remainder>-.*)$", timestamp())
  date_in_five_years = format("%d%s", local.timestamp_parts.year + 5, local.timestamp_parts.remainder)
}

resource "azurerm_monitor_action_group" "openaipoc_mac" {
  name                = "openaipoc action group"
  resource_group_name = var.resource_group_name
}

resource "azurerm_consumption_budget_resource_group" "openaipoc_budget" {
  name              = "openaipoc budget"
  resource_group_id = var.resource_group_id

  amount     = 1000
  time_grain = "Monthly"

  time_period {
    start_date = "2024-10-01T00:00:00Z"
    end_date   = local.date_in_five_years
  }

  filter {
    dimension {
      name = "ResourceId"
      values = [
        azurerm_monitor_action_group.openaipoc_mac.id,
      ]
    }
  }

  notification {
    enabled        = true
    threshold      = 50.0
    operator       = "EqualTo"
    threshold_type = "Forecasted"

    contact_emails = [
    ]

    contact_groups = [
      azurerm_monitor_action_group.openaipoc_mac.id,
    ]
  }
}

resource "azurerm_automation_account" "openaipoc_auto_account" {
  name                          = "openaipoc-automation-account"
  location                      = var.location
  resource_group_name           = var.resource_group_name
  sku_name                      = "Free"
  public_network_access_enabled = false

  identity {
    type = "SystemAssigned"
  }
}

resource "azurerm_role_assignment" "automation_contributor" {
  principal_id         = azurerm_automation_account.openaipoc_auto_account.identity[0].principal_id
  role_definition_name = "Automation Contributor"
  scope                = var.resource_group_id
}

resource "azurerm_role_assignment" "automation_aks_admin" {
  principal_id         = azurerm_automation_account.openaipoc_auto_account.identity[0].principal_id
  role_definition_name = "Azure Kubernetes Service Contributor Role"
  scope                = var.aks_cluster_id
}

locals {
  current_time              = timestamp()
  ten_minutes_in_the_future = timeadd(local.current_time, "10m")
  current_date              = formatdate("YYYY-MM-DD", local.current_time)
  tentative_stop_time       = "${local.current_date}T21:00:00Z"
  tentative_start_time      = "${local.current_date}T07:00:00Z"
  stop_time                 = timecmp(local.ten_minutes_in_the_future, local.tentative_stop_time) < 0 ? local.tentative_stop_time : timeadd(local.tentative_stop_time, "24h")
  start_time                = timecmp(local.ten_minutes_in_the_future, local.tentative_start_time) < 0 ? local.tentative_start_time : timeadd(local.tentative_start_time, "24h")
}

resource "azurerm_automation_schedule" "schedule" {
  for_each = {
    "stop" = {
      name       = "openaipoc-shutdown-schedule"
      start_time = local.stop_time
      week_days  = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"]
    }
    "start" = {
      name       = "openaipoc-startup-schedule"
      start_time = local.start_time
      week_days  = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"]
    }
  }

  name                    = each.value.name
  resource_group_name     = var.resource_group_name
  automation_account_name = azurerm_automation_account.openaipoc_auto_account.name
  frequency               = "Week"
  interval                = 1
  start_time              = each.value.start_time
  week_days               = each.value.week_days

  depends_on = [azurerm_automation_account.openaipoc_auto_account]
}

data "local_file" "startstop_aks" {
  filename = "${path.module}/StartStop-AKS.ps1"
}

resource "azurerm_automation_runbook" "aks_runbook" {
  name                    = "openaipoc-aks-runbook"
  location                = var.location
  resource_group_name     = var.resource_group_name
  automation_account_name = azurerm_automation_account.openaipoc_auto_account.name
  log_verbose             = "true"
  log_progress            = "true"
  runbook_type            = "PowerShell"

  content = data.local_file.startstop_aks.content
}

resource "azurerm_automation_job_schedule" "job_schedule" {
  for_each = {
    "stop"  = "Stop"
    "start" = "Start"
  }

  resource_group_name     = var.resource_group_name
  automation_account_name = azurerm_automation_account.openaipoc_auto_account.name
  schedule_name           = azurerm_automation_schedule.schedule[each.key].name
  runbook_name            = azurerm_automation_runbook.aks_runbook.name

  parameters = {
    aksclustername    = var.aks_cluster_name
    resourcegroupname = var.resource_group_name
    operation         = each.value
  }
}
