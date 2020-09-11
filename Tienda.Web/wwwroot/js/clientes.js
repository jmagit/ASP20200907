var Clientes = new (
    function () {
        var obj = this;
        obj.idOriginal = null;
        obj.currentPage = 1;
        obj.pagesize = 8;
        obj.getPage = false;
        obj.resetForm = function () {
            $('.msg-error').remove();
            $('#frmPrincipal').show().each(function (i, item) {
                this.reset();
            });
        };
        obj.get = function () {
            return new Promise(function (resolve, reject) {
                $.ajax({
                    url: '/api/clientes?numpage=' + (obj.currentPage - 1) + '&pagesize=' + obj.pagesize,
                    dataType: 'json',
                }).then(
                    function (resp) {
                        resolve(resp);
                    },
                    function (jqXHR, textStatus, errorThrown) {
                        reject(jqXHR, textStatus, errorThrown);
                    }
                );
            });
        };

        obj.listar = function () {
            obj.get().then(function (envios) {
                var FxP = obj.pagesize;
                $('#listado').empty()
                    .append($('<div id="content"></div>'))
                    .append($('<nav id="page-selection"></nav>'));
                $('#page-selection').twbsPagination({
                    totalPages: envios.numPag,
                    visiblePages: 10,
                    startPage: obj.currentPage,
                    first: '<i class="fas fa-angle-double-left"></i>',
                    prev: '<i class="fas fa-angle-left"></i>',
                    next: '<i class="fas fa-angle-right"></i>',
                    last: '<i class="fas fa-angle-double-right"></i>',
                    paginationClass: 'pagination justify-content-end',
                }).on('page', function (event, page) {
                    obj.currentPage = page;
                    if (obj.getPage) {
                        obj.getPage = false;
                        obj.listar();
                    } else {
                        $("#content").empty().html(Mustache.render($('#tmplListado').html(), { filas: envios.listado }));
                        obj.getPage = true;
                    }
                });
                $('#page-selection').trigger(jQuery.Event("page"), obj.currentPage);
            });
        };
        obj.añadir = function () {
            $('#listado').hide();
            obj.resetForm();
            $('#btnEnviar').on('click', obj.enviarNuevo);
        };
        obj.editar = function (id) {
            $.ajax({
                url: '/api/clientes/' + id,
                dataType: 'json',
            }).then(
                function (resp) {
                    obj.resetForm();
                    obj.idOriginal = id;
                    for (var name in resp) {
                        $('[name="' + name + '"]').each(function () {
                            if (this.type === 'radio') {
                                if (this.value === resp[name]) this.checked = true;
                            } else if (this.type === 'checkbox') {
                                if (resp[name]) this.checked = true;
                            } else
                                $(this).val(resp[name]);
                        });
                    }
                    $('#listado').hide();
                    $('#btnEnviar').on('click', obj.enviarModificado);
                }
            );
        };

        obj.borrar = function (id) {
            if (!window.confirm("¿Estas seguro?")) return;

            $.ajax({
                url: '/api/clientes/' + id,
                method: 'DELETE',
                dataType: 'json',
            }).then(
                function (resp) {
                    obj.volver();
                },
                function (jqXHR, textStatus, errorThrown) {
                    alert('ERROR: ' + jqXHR.status + ': ' + jqXHR.statusText + '</p>');
                }
            );
        };

        obj.ver = function (id) {
            $.ajax({
                url: '/api/clientes/' + id,
                dataType: 'json',
            }).then(
                function (resp) {
                    $("#listado").empty().html(Mustache.render($('#tmplDetalle').html(), { item: resp }));
                }
            );
        };

        obj.validar = function (name) {
            var cntr = $('[name="' + name + '"');
            var esValido = true;
            cntr.each(function (i, item) {
                switch (item.dataset.validacion) {
                    case 'mayusculas':
                        if (cntr.val().toUpperCase() != cntr.val())
                            item.setCustomValidity('Tiene que estar en mayusculas');
                        else
                            item.setCustomValidity('');
                        break;
                    case 'minusculas':
                        if (cntr.val().toLowerCase() != cntr.val())
                            item.setCustomValidity('Tiene que estar en minusculas');
                        else
                            item.setCustomValidity('');
                        break;
                }
                if (item.validationMessage) {
                    if ($('#err_' + name).length) {
                        $('#err_' + name).text(item.validationMessage);
                    } else {
                        cntr.after('<div id="err_' + name + '" class="text-danger msg-error">' + item.validationMessage + '</div>');
                        cntr.parent().parent().addClass('has-error');
                    }
                    esValido = false;
                } else {
                    cntr.parent().parent().removeClass('has-error');
                    $('#err_' + name).remove();
                }
            });
            return esValido;
        };

        obj.enviarNuevo = function () {
            var datos = $('#frmPrincipal').serializeArray();
            var envio = {};
            var esValido = true;
            datos.forEach(function (item) {
                if (!obj.validar(item.name)) {
                    esValido = false;
                    return;
                }
                envio[item.name] = item.value;
            });
            if (!esValido)
                return;
            $.ajax({
                url: '/api/clientes',
                method: 'POST',
                contentType: 'application/json,',
                data: JSON.stringify(envio)
            }).then(
                function () {
                    $('#btnEnviar').off('click', obj.enviarNuevo);
                    obj.volver();
                },
                function (jqXHR, textStatus, errorThrown) {
                    alert('ERROR: ' + jqXHR.status + ': ' + jqXHR.statusText + '</p>');
                }
            );
        };

        obj.enviarModificado = function () {
            $('#frmPrincipal').each(function (i, item) {
                // if(!item.checkValidity()) {
                //     alert("Error en el formulario.");
                // } else {
                var datos = $('#frmPrincipal').serializeArray();
                var envio = {};
                var esValido = true;
                datos.forEach(function (item) {
                    if (!obj.validar(item.name)) {
                        esValido = false;
                        return;
                    }
                    envio[item.name] = item.value;
                });
                if (!esValido)
                    return;
                $.ajax({
                    url: '/api/clientes/' + obj.idOriginal,
                    method: 'PUT',
                    contentType: 'application/json,',
                    data: JSON.stringify(envio)
                }).then(
                    function () {
                        $('#btnEnviar').off('click', obj.enviarModificado);
                        obj.volver();
                    },
                    function (jqXHR, textStatus, errorThrown) {
                        alert('ERROR: ' + jqXHR.status + ': ' + jqXHR.statusText);
                    }
                );
                // }
            });
        };

        obj.volver = function () {
            obj.listar();
            $('#listado').show();
            $('#frmPrincipal').hide();
        };
    }
)();