#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# docker run -it --rm -p 5000:80 --name mvcgreeting jonnycode/mvcgreeting:secondattempt
# may need prefix with winpty docker run ...

# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:latest AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY MVCWithDocker/*.csproj ./MVCWithDocker/
RUN dotnet restore

# copy everything else and build app
COPY MVCWithDocker/. ./MVCWithDocker/
WORKDIR /source/MVCWithDocker
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:latest
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "MVCWithDocker.dll"]