﻿
var globalIdFuncionario = null;
var globalNomeFuncionario = null;
console.log(globalIdFuncionario);
console.log(globalNomeFuncionario);

$(document).ready(function () {
    function getExistingFuncionarioIds() {
        var ids = [];
        $("#tabelaSelecionados tbody tr").each(function () {
            var id = $(this).find("td:nth-child(2)").text().trim();
            if (id) {
                ids.push(id);
            }
        });
        return ids;
    }

    // conforme o usuário digita no input, o código abaixo aciona o método ~/VinculosController.BuscarFuncionarios()
    $("#funcionarioInput").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "BuscarFuncionarios",
                type: "GET",
                data: { termo: request.term },
                success: function (data) {
                    var existingIds = getExistingFuncionarioIds();

                    var suggestions = data.filter(function (item) {
                        return !existingIds.includes(item.idFuncionario.toString());
                    }).slice(0, 7);

                    var mappedData = $.map(suggestions, function (item) {
                        return {
                            label: item.matriculaFuncionario + " - " + item.nomeFuncionario + " - " + item.emailFuncionario,
                            value: item.idFuncionario,
                            matricula: item.matriculaFuncionario,
                            nome: item.nomeFuncionario,
                            email: item.emailFuncionario
                        };
                    });

                    if (data.length > 7) {
                        mappedData.push({
                            label: "Continue digitando para ver mais resultados...",
                            value: null,
                            matricula: null,
                            nome: null,
                            email: null
                        });
                    }

                    response(mappedData);
                }
            });
        },
        select: function (event, ui) {
            if (ui.item.value !== null) {
                $("#funcionarioInput").val(ui.item.matricula + " - " + ui.item.nome + " - " + ui.item.email);
                globalIdFuncionario = ui.item.value;
                globalNomeFuncionario = ui.item.nome;
                console.log(globalIdFuncionario); // para testes
                console.log(globalNomeFuncionario); // para testes
            }
            return false;
        }
    });


    // limpa o input após fechar o modal
    $("#modalAviso").on("hidden.bs.modal", function () {
        $("#funcionarioInput").val("");
    });


    $("#adicionarBtn").click(function () {
        var idgestor = $("#IdGestor").val();
        var valores = $("#funcionarioInput").val().split(" - ");
        var matricula = valores[0];
        var nome = valores[1];
        var email = valores[2];

        if (matricula && nome && email) {

            // chamada AJAX para verificar se o funcionário possui um vínculo ativo:

            $.ajax({
                url: "VerificarVinculoAtivo",
                type: "GET",
                data: { idFuncionario: globalIdFuncionario },

                success: function (response) {

                    if (response.temVinculoAtivo) {

                        // exibe o modal informando que o funcionário já possui um vínculo ativo:
                        $("#mensagemModal").text(`O Funcionário ${globalNomeFuncionario} já pertence a um Grupo.`);
                        $("#modalAviso").modal("show");
                    }
                    else { 

                        // adiciona o funcionário normalmente:
                        var newRow =
                            `<tr>
                                <td>${idgestor}</td>
                                <td>${globalIdFuncionario}</td>
                                <td>${matricula}</td>
                                <td>${nome}</td>
                                <td>${email}</td>
                                <td><p>saldo aqui<p></td>
                                <td><button type="button" class="btn btn-danger remover">Remover</button></td>
                            </tr>`;

                        $("#tabelaSelecionados tbody").append(newRow);
                        $("#funcionarioInput").val("");

                    }
                }
            })


        }
    });


    // remove funcionário da tabela com delegação de eventos, considerando a class=".btn-danger" (e não o id="", que é default):
    $("#tabelaSelecionados tbody").on("click", ".btn-danger", function () {
        $(this).closest("tr").remove();
    })




    // Gravar selecionados
    $("#gravarBtn").click(function () {

        var selecionados = [];
        var timestamp = new Date().toISOString(); // Captura o timestamp no formato ISO

        $("#tabelaSelecionados tbody tr").each(function () {
            var idGestor = $(this).find("td:eq(0)").text();
            var idFuncionario = $(this).find("td:eq(1)").text();

            selecionados.push({
                IdGestor: idGestor,
                IdFuncionario: idFuncionario,
                DataInicio: timestamp
            });
        });

        console.log(selecionados); // Apenas para verificar o resultado no console

        $.ajax({
            url: "GravarGrupo",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(selecionados),
            success: function () {
                alert("Dados gravados com sucesso!");
            }
        });

    });

});
