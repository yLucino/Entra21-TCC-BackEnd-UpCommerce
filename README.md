# UpCommerce BackEnd - API Documentation

API para gerenciamento de usuários e projetos do UpCommerce. Desenvolvido em **.NET 7** com **Entity Framework Core**.

**Base URL:**  
http://localhost:5256/api

**Base URL: (swagger)**  
http://localhost:5256/swagger/index.html

---

## **User Endpoints**

| Método | Endpoint | Descrição | Body | Resposta |
|--------|----------|-----------|------|----------|
| GET | `/user` | Retorna todos os usuários registrados (sem senha) | - | ```json [{ "id": 1, "name": "username", "email": "user@email.com", "role": "admin", "urlPhoto": "", "urlLinkedin": "", "urlInstagram": "" }]``` |
| GET | `/user/{id}` | Retorna um usuário específico | - | ```json { "id": 1, "name": "username", "email": "user@email.com", "role": "admin", "urlPhoto": "", "urlLinkedin": "", "urlInstagram": "" }``` |
| POST | `/user/register` | Registra um novo usuário | ```json { "name": "username", "email": "user@email.com", "password": "123456", "role": "admin", "urlPhoto": "", "urlLinkedin": "", "urlInstagram": "" }``` | ```json { "message": "Usuário registrado com sucesso." }``` |
| POST | `/user/login` | Autentica o usuário e retorna token JWT | ```json { "email": "user@email.com", "password": "123456" }``` | ```json { "token": "JWT_TOKEN_HERE", "user": { "id": 1, "name": "username", "email": "user@email.com", "role": "admin" } }``` |
| PUT | `/user/{id}` | Atualiza os dados de um usuário | ```json { "name": "username", "email": "user@email.com", "password": "novaSenha", "role": "admin", "urlPhoto": "", "urlLinkedin": "", "urlInstagram": "" }``` | ```json { "id": 1, "name": "username", "email": "user@email.com", "role": "admin", "urlPhoto": "", "urlLinkedin": "", "urlInstagram": "" }``` |
| POST | `/user/validate-password` | Verifica se a senha enviada corresponde à senha do usuário | ```json { "userId": 1, "password": "123456" }``` | ```json { "message": "Senha válida" }``` |

---

## **Project Endpoints**

| Método | Endpoint | Descrição | Body | Resposta |
|--------|----------|-----------|------|----------|
| GET | `/project/user/{userId}` | Retorna todos os projetos de um usuário | - | ```json [{ "id": 1, "userId": 1, "title": "Projeto 1", "subTitle": "Slogan do projeto", "description": "Descrição do projeto", "urlLogo": "" }]``` |
| GET | `/project/{id}` | Retorna detalhes completos de um projeto, incluindo componentes | - | ```json { "id": 1, "userId": 1, "title": "Projeto 1", "subTitle": "Slogan do projeto", "description": "Descrição do projeto", "urlLogo": "", "component": [{ "id": "areaComponent-001", "cdkId": "homeList", "styles": { "width": 100, "height": 50 }, "children": [] }] }``` |
| POST | `/project` | Cria um novo projeto associado a um usuário | ```json { "userId": 1, "title": "Novo Projeto", "subTitle": "Slogan", "description": "Descrição do projeto", "urlLogo": "", "component": [] }``` | Retorna o projeto criado |
| PUT | `/project/{id}` | Atualiza os dados e componentes de um projeto | Mesma estrutura do POST | Retorna o projeto atualizado |
| DELETE | `/project/{id}` | Remove um projeto específico | - | HTTP 204 No Content |

---

### Observações

- Todos os endpoints de criação ou atualização de projeto devem incluir o **`userId`** para associar o projeto ao usuário.
- Senhas **nunca são retornadas** nas respostas.
- Componentes de projeto (`component`) seguem a estrutura `CdkComponent` com `styles` e `children`.

