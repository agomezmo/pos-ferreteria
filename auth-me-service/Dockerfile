FROM node:22-alpine
WORKDIR /app
COPY package.json ./
RUN npm install
COPY server.js ./
EXPOSE 3050
CMD ["node", "server.js"]
