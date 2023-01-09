FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Server/Exchange.Portal.Web/Exchange.Portal.Web.csproj", "src/Server/Exchange.Portal.Web/"]
RUN dotnet restore "src/Server/Exchange.Portal.Web/Exchange.Portal.Web.csproj"
COPY . .
WORKDIR "/src/src/Server/Exchange.Portal.Web"
RUN dotnet build "Exchange.Portal.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Exchange.Portal.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Exchange.Portal.Web.dll
