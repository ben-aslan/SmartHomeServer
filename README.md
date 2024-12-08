<center>

# Smart Home Server

<b>MQTT and Telegram [SmartHome](https://github.com/ben-aslan/SmartHome)</b><br>
<sub>(Tested on Ubuntu 22.04 & 24.04)</sub>

</center>

<hr>

## Table of Contents

- [Installation](#installation)

## Installation

Installation with docker:

Install docker

```bash
curl -fsSL https://get.docker.com | sh
```

```bash
mkdir /opt/SmartHomeServer
cd /opt/SmartHomeServer
```

```bash
curl -O -L "https://raw.githubusercontent.com/ben-aslan/SmartHomeServer/main/docker-compose.yml";
```

```bash
mkdir Logs
```

```bash
docker compose up -d
```
