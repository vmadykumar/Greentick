FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY MasterMgmt/MasterMgmt.csproj MasterMgmt/
COPY MasterMgmt.CommonLayer/MasterMgmt.CommonLayer.csproj MasterMgmt.CommonLayer/
COPY MasterMgmt.BuisnessLayer/MasterMgmt.BusinessLayer.csproj MasterMgmt.BuisnessLayer/
COPY MasterMgmt.DataLayer/MasterMgmt.DataLayer.csproj MasterMgmt.DataLayer/
COPY MasterMgmt.Exceptions/MasterMgmt.Exceptions.csproj MasterMgmt.Exceptions/
RUN dotnet restore MasterMgmt/MasterMgmt.csproj
COPY . .
WORKDIR /src/MasterMgmt
RUN dotnet build MasterMgmt.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MasterMgmt.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MasterMgmt.dll"]
