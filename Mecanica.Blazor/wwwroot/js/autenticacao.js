window.autenticacaoOficina = {
    async login(login, senha) {
        const resposta = await fetch("/auth/login", {
            method: "POST",
            credentials: "same-origin",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                login: login,
                senha: senha
            })
        });

        if (resposta.ok) {
            return {
                sucesso: true,
                mensagem: "Login realizado com sucesso!"
            };
        }

        let mensagem = "Não foi possível realizar o login.";

        try {
            const erro = await resposta.json();
            mensagem = erro.mensagem ?? mensagem;
        } catch {
            // Mantém a mensagem padrão.
        }

        return {
            sucesso: false,
            mensagem: mensagem
        };
    }
};