FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY DFC.Sitefinity.MicroService.API.sln ./
COPY DFC.Sitefinity.MicroService.API/DFC.Sitefinity.MicroService.API.csproj DFC.Sitefinity.MicroService.API/
RUN dotnet restore DFC.Sitefinity.MicroService.API/DFC.Sitefinity.MicroService.API.csproj -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/DFC.Sitefinity.MicroService.API
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DFC.Sitefinity.MicroService.API.dll"]
