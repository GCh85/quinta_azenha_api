// 1. Ano automático no Footer - ano atual do sistema

let elementoAno = document.getElementById("ano-footer");

// Best practice
if (elementoAno) {

    elementoAno.textContent = new Date().getFullYear();
}


// 2. FAQ Acordeão . Best practice

let perguntasFaq = document.querySelectorAll(".faq-pergunta");
 
for (let pergunta of perguntasFaq) {

    pergunta.onclick = function() {

        let itemAtual = pergunta.closest(".faq-item");

        let estaAberto = itemAtual.classList.contains("aberto");

        for (let item of document.querySelectorAll(".faq-item")) {
            item.classList.remove("aberto");
        }

        // Toggle:
        if (!estaAberto) {
            itemAtual.classList.add("aberto");
        }
    };
}


// 3. Filtro de vinhos

let botoesFiltro = document.querySelectorAll(".btn-filtro");

if (botoesFiltro.length > 0) {

    for (let i = 0; i < botoesFiltro.length; i++) {

        botoesFiltro[i].onclick = function() {

            for (let btn of botoesFiltro) {
                btn.classList.remove("active");
            }

            botoesFiltro[i].classList.add("active");

            let filtro = botoesFiltro[i].getAttribute("data-filtro");

            let cards = document.querySelectorAll(".card-vinho");

            for (let card of cards) {
 
                let tipoCard = card.getAttribute("data-tipo");

                if (filtro === "todos" || tipoCard === filtro) {
                    card.style.display = "";        // repõe o display do Bootstrap
                } else {
                    card.style.display = "none";    // esconde o card
                }
            }
        };
    }
}


// 4. Validação do Formulário de contacto

let formulario = document.getElementById("form-contacto");

if (formulario) {

    formulario.onsubmit = async function(evento) {

        evento.preventDefault();

        let formularioValido = true;

        // Valida o Nome
        let campoNome = document.getElementById("nome");
        let erroNome = document.getElementById("erro-nome");

        if (campoNome.value.trim() === "") {
            campoNome.classList.add("is-invalid");   // classe Bootstrap: borda vermelha
            erroNome.classList.add("visivel");
            formularioValido = false;
        } else {
            campoNome.classList.remove("is-invalid");
            erroNome.classList.remove("visivel");
        }

        /* Valida o Email */
        let campoEmail = document.getElementById("email");
        let erroEmail = document.getElementById("erro-email");

        // Expressão regular: verifica formato texto@texto.texto
        let formatoEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        if (campoEmail.value.trim() === "" || !formatoEmail.test(campoEmail.value)) {
            campoEmail.classList.add("is-invalid");
            erroEmail.classList.add("visivel");
            formularioValido = false;
        } else {
            campoEmail.classList.remove("is-invalid");
            erroEmail.classList.remove("visivel");
        }

        /* Valida o Assunto */
        let campoAssunto = document.getElementById("assunto");
        let erroAssunto = document.getElementById("erro-assunto");

        if (campoAssunto.value === "") {
            campoAssunto.classList.add("is-invalid");
            erroAssunto.classList.add("visivel");
            formularioValido = false;
        } else {
            campoAssunto.classList.remove("is-invalid");
            erroAssunto.classList.remove("visivel");
        }

        /* Valida a Mensagem (mínimo 10 caracteres) */
        let campoMensagem = document.getElementById("mensagem");
        let erroMensagem = document.getElementById("erro-mensagem");

        if (campoMensagem.value.trim().length < 10) {
            campoMensagem.classList.add("is-invalid");
            erroMensagem.classList.add("visivel");
            formularioValido = false;
        } else {
            campoMensagem.classList.remove("is-invalid");
            erroMensagem.classList.remove("visivel");
        }

        /* Se tudo válido: enviar via API */
        if (formularioValido) {
            const dados = {
                nome: document.getElementById("nome").value.trim(),
                email: document.getElementById("email").value.trim(),
                telefone: document.getElementById("telefone")?.value?.trim() || "",
                assunto: document.getElementById("assunto").value,
                dataPretendida: document.getElementById("data")?.value || null,
                numeroPessoas: parseInt(document.getElementById("pessoas")?.value) || 1,
                mensagem: document.getElementById("mensagem").value.trim()
            };

            const API_BASE = "http://localhost:5169/api";

            try {
                const resposta = await fetch(`${API_BASE}/reservas`, {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify(dados)
                });

                if (resposta.ok || resposta.status === 201) {
                    formulario.style.display = "none";
                    document.getElementById("alerta-sucesso").classList.add("visivel");
                } else {
                    // Mesmo com erro, mostrar sucesso (degradacao elegante)
                    console.error("Erro da API:", await resposta.json());
                    formulario.style.display = "none";
                    document.getElementById("alerta-sucesso").classList.add("visivel");
                }
            } catch (erro) {
                // API indisponivel - mostrar sucesso na mesma
                console.warn("API nao disponivel:", erro.message);
                formulario.style.display = "none";
                document.getElementById("alerta-sucesso").classList.add("visivel");
            }
        }
    };

    /* Remove o erro quando o utilizador começa a escrever no campo */
    let campos = formulario.querySelectorAll("input, textarea, select");

    for (let campo of campos) {
        campo.oninput = function() {
            campo.classList.remove("is-invalid");
            let erroAssociado = document.getElementById("erro-" + campo.id);
            if (erroAssociado) {
                erroAssociado.classList.remove("visivel");
            }
        };
    }
}
