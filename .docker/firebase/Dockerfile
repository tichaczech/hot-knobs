# Inspiration: https://github.com/andreyka26-git/firebase-docker-emulators/blob/main/common-firebase-emulator/Dockerfile

FROM node:24-slim

RUN apt-get update -y && \
    apt-get install -y openjdk-17-jdk ca-certificates-java && \
    apt-get clean && \
    update-ca-certificates -f

RUN npm install -g firebase-tools

WORKDIR /app

COPY . .

RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]

# firebase-emulator:
#   build:
#     context: .docker/firebase
#     dockerfile: Dockerfile
#   container_name: fand-runtime-firebase-emulator
#   healthcheck:
#     test: ["CMD", "curl", "-f", "http://localhost:4000"]
#     interval: 5s
#     retries: 6
#   hostname: firebase-emulator
#   restart: always
#   # volumes:
#   #   - ./_data/firebase-emulator:/data
#   networks:
#     - default
#   ports:
#     - "4000:4000"
#     - "9199:9199"
