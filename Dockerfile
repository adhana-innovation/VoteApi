# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project files and restore dependencies
COPY *.sln ./
COPY ["MyVoteOnline.Api/MyVoteOnline.Api.csproj", "./MyVoteOnline.Api/"]
COPY ["MyVoteOnline.Services/MyVoteOnline.Services.csproj", "./MyVoteOnline.Services/"]
COPY ["MyVotOnline.DataBaseLayer/MyVotOnline.DataBaseLayer.csproj", "./MyVotOnline.DataBaseLayer/"]
COPY ["MyVotOnline.Model/MyVotOnline.Model.csproj", "./MyVotOnline.Model/"]

RUN dotnet restore "MyVoteOnline.Api/MyVoteOnline.Api.csproj"

# Copy the entire source code and build
COPY . .
RUN dotnet publish "MyVoteOnline.Api/MyVoteOnline.Api.csproj"  -c Release -o /out

# Use the official ASP.NET Core runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Expose ports for HTTP (5000) and optionally HTTPS (5001)
ENV PORT=5000
EXPOSE 5000

# Run the application
ENTRYPOINT ["dotnet", "MyVoteOnline.Api.dll"]
