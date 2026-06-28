# https://hub.docker.com/r/microsoft/devcontainers-dotnet

# TODO: Use slim version when available for .NET 10
# FROM mcr.microsoft.com/devcontainers/dotnet:10.0-preview-trixie-slim
FROM mcr.microsoft.com/devcontainers/dotnet:10.0

# Rosetta support
RUN if [[ "$(uname -m)" ==  "arm64" ]]; then \
    sudo dpkg --add-architecture amd64; \
    sudo apt-get update; \
    sudo apt-get install binutils:amd64; \
fi

# [Optional] Uncomment this line to install global node packages.
# RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g <your-package-here>" 2>&1

# NODE_MODULES="tslint-to-eslint-config typescript"
# RUN su node -c "umask 0002 && npm install -g ${NODE_MODULES}" && npm cache clean --force > /dev/null 2>&1

# Hotfix for permissions of .local folder, which is used for global npm and dotnet tools installation.
RUN sudo mkdir -p /home/vscode/.local
RUN sudo chown -R vscode:vscode /home/vscode/.local

# Hotfix for permissions of non existent directories in mapped volume, which are created with root permissions.
RUN sudo mkdir -p /home/vscode/.nuget/NuGet
RUN sudo chown -R vscode:vscode /home/vscode/.nuget
