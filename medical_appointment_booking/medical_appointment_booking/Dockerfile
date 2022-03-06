#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["medical_appointment_booking/medical_appointment_booking.csproj", "medical_appointment_booking/"]
RUN dotnet restore "medical_appointment_booking/medical_appointment_booking.csproj"
COPY . .
WORKDIR "/src/medical_appointment_booking"
RUN dotnet build "medical_appointment_booking.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "medical_appointment_booking.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "medical_appointment_booking.dll"]