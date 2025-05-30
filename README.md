# 🏗️ Módulo de Cadastro

O módulo de Cadastro é responsável por gerenciar a identidade dos usuários, autenticação e informações cadastrais. Ele centraliza os processos de criação, atualização e consulta de clientes, além de oferecer um fluxo seguro de autenticação utilizando JWT e controle de credenciais.

## 🔥 Funcionalidades atuais
- 🔐 **Autenticação e Gestão de Credenciais**
  - Criação de credenciais seguras com criptografia BCrypt.
  - Login com geração de token JWT.
  - Registro de sessões com controle de expiração.

- 📦 **Gestão de Clientes**
  - Cadastro, edição e consulta de clientes.

- 🔑 **Validação via OTP**
  - Geração e validação de códigos OTP (Email e SMS - interno).
  - Controle para não gerar múltiplos códigos ativos.
  - Expiração configurável e limite de tentativas.

## 🔐 Segurança
- Criptografia de senhas (BCrypt).
- Autenticação JWT com claims customizadas.
- Controle de sessões e expiração.
- Preparado para expansão com MFA.

## 🚀 Melhorias planejadas
- Refresh Token.
- Rate Limiting.
- Bloqueio temporário após tentativas inválidas.
- MFA (Autenticação multifator).
- Integração com serviços externos (email, SMS).
- Logs e rastreabilidade.

## 🏛️ Arquitetura
- Clean Architecture:
  - Application: Handlers, Interfaces, Requests, Responses.
  - Domain: Entities, Enums.
  - Infra: Repositories (MongoDB).
  - API: Controllers, Middlewares.

## 🚀 Pré-requisitos
- .NET 8 SDK
- MongoDB
- Docker (opcional)

## 🏗️ Rodando localmente
1. Clone o projeto
2. Configure o appsettings.json
3. Execute: `dotnet run --project ModuloCadastro.API`

## 🐳 Docker MongoDB
```yaml
version: '3'
services:
  mongodb:
    image: mongo
    ports:
      - "27017:27017"
    volumes:
      - ./data:/data/db
