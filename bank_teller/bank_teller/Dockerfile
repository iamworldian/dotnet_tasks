FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["bank_teller/bank_teller.csproj", "bank_teller/"]
RUN dotnet restore "bank_teller/bank_teller.csproj"
COPY . .
WORKDIR "/src/bank_teller"
RUN dotnet build "bank_teller.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "bank_teller.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "bank_teller.dll"]
