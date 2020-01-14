FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app
COPY *.sln .
COPY MasterMgmt/MasterMgmt.csproj ./MasterMgmt/
COPY MasterMgmt.BuisnessLayer/MasterMgmt.BusinessLayer.csproj ./MasterMgmt.BuisnessLayer/
COPY MasterMgmt.CommonLayer/MasterMgmt.CommonLayer.csproj ./MasterMgmt.CommonLayer/
COPY MasterMgmt.DataLayer/MasterMgmt.DataLayer.csproj ./MasterMgmt.DataLayer/
COPY MasterMgmt.Exceptions/MasterMgmt.Exceptions.csproj ./MasterMgmt.Exceptions/
COPY MasterMgmt.UnitTest/MasterMgmt.UnitTest.csproj ./MasterMgmt.UnitTest/
RUN dotnet restore
COPY MasterMgmt/. ./MasterMgmt/
COPY MasterMgmt.BuisnessLayer/. ./MasterMgmt.BuisnessLayer/
COPY MasterMgmt.CommonLayer/. ./MasterMgmt.CommonLayer/
COPY MasterMgmt.DataLayer/. ./MasterMgmt.DataLayer/
COPY MasterMgmt.Exceptions/. ./MasterMgmt.Exceptions/
COPY MasterMgmt.UnitTest/. ./MasterMgmt.UnitTest.csproj/
WORKDIR /app/MasterMgmt
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/MasterMgmt/out ./
ENTRYPOINT ["dotnet", "MasterMgmt.dll"]