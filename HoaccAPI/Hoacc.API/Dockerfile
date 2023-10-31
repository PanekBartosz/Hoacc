FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HoaccAPI/HoaccAPI.csproj", "HoaccAPI/"]
RUN dotnet restore "HoaccAPI/HoaccAPI.csproj"
COPY . .
WORKDIR "/src/HoaccAPI"
RUN dotnet build "HoaccAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HoaccAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HoaccAPI.dll"]
