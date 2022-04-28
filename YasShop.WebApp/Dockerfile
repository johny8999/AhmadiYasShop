#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["YasShop.WebApp/YasShop.WebApp.csproj", "YasShop.WebApp/"]
COPY ["YasShop.WebApp.Core/YasShop.WebApp.Core.csproj", "YasShop.WebApp.Core/"]
COPY ["YasShop.Infrastructure.core/YasShop.Infrastructure.Core.csproj", "YasShop.Infrastructure.core/"]
COPY ["YasShop.Infrastructure.EfCore.Identity/YasShop.Infrastructure.EfCore.Identity.csproj", "YasShop.Infrastructure.EfCore.Identity/"]
COPY ["YasShop.Domain/YasShop.Domain.csproj", "YasShop.Domain/"]
COPY ["Framework/Framework.csproj", "Framework/"]
COPY ["YasShop.Infrastructure.EfCore/YasShop.Infrastructure.EfCore.csproj", "YasShop.Infrastructure.EfCore/"]
COPY ["YasShop.Infrastructure.EfCore.Identity.JWT/YasShop.Infrastructure.EfCore.Identity.JWT.csproj", "YasShop.Infrastructure.EfCore.Identity.JWT/"]
RUN dotnet restore "YasShop.WebApp/YasShop.WebApp.csproj"
COPY . .
WORKDIR "/src/YasShop.WebApp"
RUN dotnet build "YasShop.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YasShop.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YasShop.WebApp.dll"]