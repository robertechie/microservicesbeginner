FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/publish .
#ENTRYPOINT ["dotnet", "DecaBlog.dll"]
CMD ASPNETCORE_URLS=htcleartp://*:$PORT dotnet my-test.dll