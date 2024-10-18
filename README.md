# BancoChu
A aplicação precisa ter instalado na máquina o Visual Studio e o Docker desktop configurado para rodar container linux.
Ao abrir a aplicação ela já vai conectar no docker, mandado rodar a aplicação ele irá criar o container no docker e subir o swagger da aplicação.
As principais funções estão presentes nas rotas de cadas seção.
Existe duas rotas que foram desenvolvidas para testes da aplicação, elas realizam o cadastros de contas e o cadastro de movimentações, asssm é possível consulta e gerar o extrato;
Essas rotas seriam /CreateContaPadrao e /CreateMovimentacaoPadrao

Para rodar os testes, é só estar com a aplicação no visual Studio e mandar rodar os testes, ele já dispara os arquivos de testes das principais funcionalidade de controle de conta movimentação e extrato.
