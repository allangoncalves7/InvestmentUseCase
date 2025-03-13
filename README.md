# Investment Use Case API

## Visão Geral
Esta API fornece informações sobre o saldo de um cliente nos diversos fundos de investimento. O sistema recebe os dados da agência, conta e DAC e retorna informações sobre os produtos de investimento do cliente.

## Tecnologias Utilizadas
- .NET 8
- PostgreSQL
- Docker & Docker Compose
- Prometheus & Grafana

## Como Executar o Projeto

### Requisitos
Certifique-se de ter instalado:
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

### Passos para execução
1. Clone o repositório:
   ```sh
   git clone [https://github.com/seu-usuario/investment-use-case-api.git](https://github.com/allangoncalves7/InvestmentUseCase.git)
   cd InvestmentUseCase
   ```

2. Execute o Docker Compose:
   ```sh
   docker-compose up --build
   ```
   Isso iniciará os serviços:
   - **PostgreSQL** (Banco de dados relacional)
   - **API .NET 8** (Servidor de aplicação)
   - **Prometheus** (Coleta de métricas)
   - **Grafana** (Dashboard de visualização)

3. A API estará disponível em:
   ```sh
   http://localhost:8080
   ```

4. Para verificar os logs:
   ```sh
   docker logs -f investment_api
   ```

## Endpoints Principais

### 1. Consultar Saldo de Investimentos
**GET /api/Investment/GetByCustomerAndProduct**
**GET /api/Investment/GetByCustomer**
Consulta um invetimento em um produto ou todos investimento pela agência, conta e DAC

### 2. Depósito ou Reesgate
**PUT /api/Investment/UpdateInvestedCapital**
Realiza depósito ou resgate no investimento de acordo com a ação. 0 = Depósioto, 1 = Resgate.

## Configuração do Grafana

1. Acesse o Grafana em `http://localhost:3000`
2. Faça login com usuário `admin` e senha `admin`
3. Crie o DataSource e selecione Prometheus, na url insira : http://prometheus:9090
4. Importe o dashboard JSON localizado em `grafana/dashOpenTelemetry.json`
5. Se as métricas não carregarem automaticamente:
   - Clique em **Editar Painel**
   - Clique em **Run Query**

## Observabilidade
A API possui suporte para:
- **Logging** com Serilog
- **Tracing** com OpenTelemetry
- **Métricas** com Prometheus

