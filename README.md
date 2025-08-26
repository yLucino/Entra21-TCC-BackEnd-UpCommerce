# UpCommerce BackEnd - API Documentation

API para gerenciamento de usuários e projetos do UpCommerce. Desenvolvido em **.NET 7** com **Entity Framework Core**.

**Base URL:**  
http://localhost:5256/api

**Base URL (Swagger):**  
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
| GET | `/project/user/{userId}` | Retorna todos os projetos de um usuário | - | ```json [{ "id": 1, "title": "Projeto 1", "subTitle": "Slogan do projeto", "description": "Descrição do projeto", "urlLogo": "" }]``` |
| GET | `/project/user/{userId}/{projectId}` | Retorna detalhes completos de um projeto, incluindo componentes | - | ```json { "id": 1, "title": "Projeto 1", "subTitle": "Slogan do projeto", "description": "Descrição do projeto", "urlLogo": "", "userId": 1, "component": [{ "id": "areaComponent-001", "cdkId": "homeList", "children": [], "style": {...} }] }``` |
| POST | `/project/user/{userId}` | Cria um novo projeto associado a um usuário | ```json { "title": "Novo Projeto", "subTitle": "Slogan", "description": "Descrição do projeto", "urlLogo": "", "component": [] }``` | ```json { "message": "Projeto criado com sucesso.", "projectId": 1 }``` |
| PUT | `/project/user/{userId}/{projectId}` | Atualiza os dados e componentes de um projeto | Mesma estrutura do POST | Retorna o projeto atualizado, incluindo `component` como JSON |
| DELETE | `/project/user/{userId}/{projectId}` | Remove um projeto específico | - | ```json { "message": "Projeto deletado com sucesso." }``` |

---

### Observações

- **Componentes do projeto (`component`)** são armazenados como JSON (`ComponentJson`) no banco de dados.  
- GET de todos os projetos (`/project/user/{userId}`) retorna **apenas os campos principais** (`id`, `title`, `subTitle`, `description`, `urlLogo`).  
- Componentes podem ser tratados pelo frontend diretamente em JSON, sem FK ou tabelas separadas.  
- Senhas **nunca são retornadas** nas respostas.  
