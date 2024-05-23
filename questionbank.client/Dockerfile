# Stage 1: Build the application
FROM node:16 AS build

WORKDIR /app

COPY package.json package-lock.json ./
RUN npm install

COPY . .
RUN npm run build

# Stage 2: Serve the application with a lightweight web server
FROM nginx:alpine

COPY nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80

RUN printenv

CMD ["nginx", "-g", "daemon off;"]
