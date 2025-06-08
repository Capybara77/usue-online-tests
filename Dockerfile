ROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY . .

RUN dotnet restore "./usue-online-tests/usue-online-tests.csproj"

WORKDIR "/src/."
RUN dotnet publish "usue-online-tests/usue-online-tests.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

RUN apt-get update && \
    apt-get install -y --no-install-recommends libfontconfig1 libfreetype6 && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "usue-online-tests.dll"]