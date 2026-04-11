# TODO: Use slim version when available for .NET 10
# FROM mcr.microsoft.com/devcontainers/dotnet:9.0-bookworm-slim
FROM mcr.microsoft.com/devcontainers/dotnet:10.0

# [Optional] Uncomment this section to install additional OS packages.
RUN apt-get update && export DEBIAN_FRONTEND=noninteractive && apt-get -y install --no-install-recommends inetutils-ping python3-pip

# [Optional] Uncomment this line to install global node packages.
# RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g <your-package-here>" 2>&1

# NODE_MODULES="tslint-to-eslint-config typescript"
# RUN su node -c "umask 0002 && npm install -g ${NODE_MODULES}" && npm cache clean --force > /dev/null 2>&1

# [Fix] For details see https://github.com/devcontainers/features/issues/1543
USER root
RUN if [ -f /etc/apt/sources.list.d/yarn.list ]; then \
    mv /etc/apt/sources.list.d/yarn.list /etc/apt/sources.list.d/yarn.list.disabled; \
fi
RUN if [ -f /usr/share/keyrings/yarn.gpg ]; then \
    mv /usr/share/keyrings/yarn.gpg /usr/share/keyrings/yarn.gpg.disabled; \
fi
RUN apt-get update
