FROM node:14.13.1-buster AS build
WORKDIR /usr/
COPY package.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:1.17.3-alpine AS base
WORKDIR /usr/share/nginx/html
COPY --from=build /usr/dist/game-loan-manager-web .
COPY ./nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80