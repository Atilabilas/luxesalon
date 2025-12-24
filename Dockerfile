# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Çözüm ve proje dosyalarını kopyala
COPY ["LuxeSalon.sln", "./"]
COPY ["LuxeSalon.Core/LuxeSalon.Core.csproj", "LuxeSalon.Core/"]
COPY ["LuxeSalon.Infrastructure/LuxeSalon.Infrastructure.csproj", "LuxeSalon.Infrastructure/"]
COPY ["LuxeSalon.Web/LuxeSalon.Web.csproj", "LuxeSalon.Web/"]

# Bağımlılıkları geri yükle
RUN dotnet restore "LuxeSalon.Web/LuxeSalon.Web.csproj"

# Tüm kaynak kodları kopyala
COPY . .

# Uygulamayı derle
WORKDIR "/src/LuxeSalon.Web"
RUN dotnet build "LuxeSalon.Web.csproj" -c Release -o /app/build

# Publish aşaması
FROM build AS publish
RUN dotnet publish "LuxeSalon.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# App_Data klasörünü oluştur (SQLite için)
RUN mkdir -p /app/App_Data

# Publish çıktısını kopyala
COPY --from=publish /app/publish .

# Port ayarı
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Uygulamayı başlat
ENTRYPOINT ["dotnet", "LuxeSalon.Web.dll"]
