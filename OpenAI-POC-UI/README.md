
# OpenAI POC UI

This project is a React-based UI for OpenAI's Proof of Concept. Below are instructions on how to set up your environment using NVM (Node Version Manager), install dependencies, and run the project, as well as build and run it using Docker.

## Prerequisites

### 1. Node Version Management with NVM (for Windows users)

Ensure that you are using the correct Node.js version for this project. The version is specified in the `.nvmrc` file.

1. You must have **NVM for Windows** installed. You can download it [here](https://github.com/coreybutler/nvm-windows/releases).
   
2. After installing NVM, open a terminal and run:

   ```bash
   nvm install
   ```

   This will install the version of Node.js specified in the `.nvmrc` file (e.g., `20.18.0`).

3. If the specific version (e.g., `20.18.0`) is not installed, you can manually install it by running:

   ```bash
   nvm install 20.18.0
   ```

4. Activate the correct version of Node.js by running:

   ```bash
   nvm use
   ```

   Or, if that doesn't work:

   ```bash
   nvm use 20.18.0
   ```

5. Verify the active Node.js version by running:

   ```bash
   node -v
   ```

   It should return `v20.18.0`.

### 2. Install Dependencies

To install the necessary dependencies for the project, run:

```bash
npm install
```

This will install all the packages listed in the `package.json` file.

### 3. Run the Development Server

To start the development server, run:

```bash
npm run dev
```

This will start the Vite development server, and you can access the app at `http://localhost:5173`.

### 4. Build for Production

To build the project for production, use the following command:

```bash
npm run build
```

This will generate the static files in the `dist` directory.

### 5. Preview the Production Build

To preview the production build locally, use the following command:

```bash
npm run preview
```

This will serve the production build for you to preview at `http://localhost:4173`.

## Docker Setup

### Build the Docker Image

To build a Docker image for this project, run the following command:

```bash
docker build -t openai-poc-ui .
```

### Run the Docker Container

To run the project in a Docker container, use:

```bash
docker run -p 8080:8080 openai-poc-ui
```

This will run the app and expose it on `http://localhost:8080`.

### Environment Variables

- **PORT**: The port where the application will run (default is `8080`).
- **BUILD_CONFIGURATION**: The build configuration (default is `production`).

## Project Structure

- **Main Dependencies**:
  - `react`: ^18.3.1
  - `react-dom`: ^18.3.1
  - `react-router-dom`: ^6.27.0
- **Dev Dependencies**:
  - `vite`: ^5.4.8
  - `typescript`: ^5.5.3
  - `eslint`: ^9.11.1
  - `tailwindcss`: ^3.4.14
  - `typescript-eslint`: ^8.7.0
  - and others...

## Scripts

- `npm run dev`: Start the development server.
- `npm run build`: Build the project for production.
- `npm run lint`: Lint the project using ESLint.
- `npm run preview`: Preview the production build.
