# :moneybag: Projeto Byte Bank

Esse projeto foi feito como atividade para a formação no Programa Sharp Coders da Ímã Learning Place. O projeto consiste em um sistema bancário simples desenvolvido em C# como uma aplicação console. Cada cliente possui um CPF, nome, senha e saldo. Os dados foram guardados em arquivo JSON. Foi utilizada Programação Orientada a Objetos para organização e manutenção do código.

## ✅ Principais Funcionalidades Menu Principal

- **Inserir novo Usuário**: é possível inserir um novo usuário de acordo com as condições abaixo.
  - Validação do CPF: CPF deve conter 11 dígitos numéricos e não deve existir outro CPF igual no arquivo JSON.
  - Validação do Nome: nome deve conter no mínimo 2 e no máximo 60 caracteres que seja apenas letras ou espaços.
  - Validação da Senha: senha deve conter no mínimo 5 e no máximo 50 caracteres.
- **Deletar um Usuário**: é possível deletar um usuário existente no arquivo JSON.
  - Validação do CPF: o CPF digitado deve existir no arquivo JSON.
  - Validação da Senha: a senha digitada deve ser a mesma da conta cujo CPF foi informado.
- **Listar todas as contas registradas**: é possível ver os CPFs, nomes e saldos de todas as contas do arquivo JSON.
- **Detalhes de um Usuário**: é possível ver o CPF, nome, senha e saldo de uma conta existente no arquivo JSON.
  - Validação do CPF: o CPF digitado deve existir no arquivo JSON.
- **Quantia armazenada no banco**: é possível ver a soma dos saldos de todas as contas existentes no arquivo JSON.
- **Manipular conta**: as funcionalidades dessa opção são descritas no vídeo ByteBank.mp4 presente neste repositório.

### :exclamation: O projeto está em andamento e será melhorado durante o curso.

