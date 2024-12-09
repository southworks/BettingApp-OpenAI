FROM node:20 AS build
ARG VITE_SERVER_DOMAIN
ENV VITE_SERVER_DOMAIN=${VITE_SERVER_DOMAIN}
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build

FROM node:20 AS production
WORKDIR /app

COPY --from=build /app/dist ./dist

RUN npm install -g serve


ENV PORT=8080
ENV BUILD_CONFIGURATION=$BUILD_CONFIGURATION

EXPOSE ${PORT}

CMD ["sh", "-c", "serve -s dist -l $PORT"]
