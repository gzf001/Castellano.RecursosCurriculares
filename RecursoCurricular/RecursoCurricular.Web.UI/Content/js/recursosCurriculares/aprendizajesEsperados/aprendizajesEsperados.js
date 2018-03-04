jQuery(document).ready(function () {

    var contenidosId = [];
    var objetivosVerticalesId = [];

    var tableAprendizaje;
    var tableIndicador;

    $('#aprendizajeForm').hide();

    $('#indicadorForm').hide();

    $('#tipoEducacion').change(function (e) {

        e.preventDefault();

        $("#grado").html("<option value='-1'>[Seleccione]</option>");
        $("#sector").html("<option value='-1'>[Seleccione]</option>");

        var tipoEducacion = $('#tipoEducacion').val();

        $.getJSON('/Educacion/Home/Grados/' + tipoEducacion, function (data) {

            var items = "";

            $.each(data, function (i, grado) {
                items += "<option value='" + grado.Value + "'>" + grado.Text + "</option>";
            });

            $("#grado").html(items);
        });

        gridViewAprendizaje();
    })

    $('#grado').change(function (e) {

        e.preventDefault();

        $("#sector").html("<option value='-1'>[Seleccione]</option>");

        if ($('#grado').val() > 0) {

            $.getJSON('/Educacion/Home/Sectores/', function (data) {

                var items = "";

                $.each(data, function (i, sector) {
                    items += "<option value='" + sector.Value + "'>" + sector.Text + "</option>";
                });

                $("#sector").html(items);
            });
        }

        gridViewAprendizaje();
    })

    $('#sector').change(function (e) {

        e.preventDefault();

        tableAprendizaje = gridViewAprendizaje();
    })

    $('#accordion').accordion({
        collapsible: true,
        heightStyle: "content"
    });

    $(document).on('click', 'a[id=addAprendizaje]', function (e) {

        e.preventDefault();

        $.getJSON("/RecursosCurriculares/AprendizajeEsperado/AddAprendizaje/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val(), function (data) {

            if (data === "500") {

                swal("Error!", "Seleccione el sector", "error");
            }
            else {

                $(":ui-fancytree").fancytree("destroy");

                $('#aprendizajeId').val(data.Id);
                $('#aprendizajeTipoEducacion').val(data.TipoEducacionNombre);
                $('#aprendizajeGrado').val(data.GradoNombre);
                $('#aprendizajeSector').val(data.SectorNombre);
                $('#aprendizajeNumero').val(data.Numero);
                $('#aprendizajeDescripcion').val(data.Descripcion);

                tableIndicador = gridViewIndicador();

                treeContenidos(contenidosId);

                treeObjetivosVerticales(objetivosVerticalesId);

                $('#indicadorForm').hide();

                $('#form').hide(500);

                $('#aprendizajeForm').show(500);
            }
        })
    })

    $(document).on('click', 'a[id=addIndicador]', function (e) {

        e.preventDefault();

        $.getJSON("/RecursosCurriculares/AprendizajeEsperado/AddIndicador/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#aprendizajeId').val(), function (data) {

            if (data === "500") {

                swal("Error!", "Existen problemas al agregar un nuevo indicador, por favor refresque el navegador y reintente", "error");
            }
            else {
                $('#indicadorId').val(data.IndicadorItem.Id)
                $('#tipoEducacionIndicadorCodigo').val(data.TipoEducacionNombre);
                $('#gradoIndicadorCodigo').val(data.GradoNombre);
                $('#sectorIndicadorId').val(data.SectorNombre);
                $('#aprendizajeIndicadorNumero').val(data.IndicadorItem.Numero);
                $('#aprendizajeIndicadorDescripcion').val(data.Descripcion);
                $('#aprendizajeIndicadorCategoria').val('-1');
                $('#indicadorDescripcion').val(data.IndicadorItem.Descripcion);

                popUp();
            }
        });
    })

    $(document).on('click', 'a[id=editAprendizaje]', function (e) {

        e.preventDefault();

        $.getJSON("/RecursosCurriculares/AprendizajeEsperado/EditAprendizaje/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $(this).attr('data-value'), function (data) {

            if (data === "500") {

                swal("Error!", "Existen problemas al editar el aprendizaje, por favor refresque el navegador y reintente", "error");
            }
            else {

                $(":ui-fancytree").fancytree("destroy");

                $('#aprendizajeId').val(data.Id);
                $('#aprendizajeTipoEducacion').val(data.TipoEducacionNombre);
                $('#aprendizajeGrado').val(data.GradoNombre);
                $('#aprendizajeSector').val(data.SectorNombre);
                $('#aprendizajeNumero').val(data.Numero);
                $('#aprendizajeDescripcion').val(data.Descripcion);

                tableIndicador = gridViewIndicador();

                treeContenidos(contenidosId);

                treeObjetivosVerticales(objetivosVerticalesId);

                $('#indicadorForm').show(500);

                $('#form').hide(500);

                $('#aprendizajeForm').show(500);
            }
        });
    })

    $(document).on('click', 'a[id=deleteAprendizaje]', function (e) {

        e.preventDefault();

        var id = $(this).attr('data-value');

        swal({
            title: "¿Esta seguro?",
            text: "Se eliminará el aprendizaje",
            type: "warning",
            showCancelButton: true,
            cancelButtonText: "Cancelar",
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Si, eliminalo",
            closeOnConfirm: false
        },
            function () {

                $.ajax({
                    type: 'GET',
                    url: '/RecursosCurriculares/AprendizajeEsperado/DeleteAprendizaje/' + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + id,
                    success: function (data) {

                        if (data === "200") {

                            tableAprendizaje.ajax.reload();

                            swal("Eliminado!", "El aprendizaje fue eliminado de forma correcta", "success");
                        }
                        else {

                            swal("Error!", "El aprendizaje no puede ser eliminado", "error");
                        }
                    },
                    error: function (data) {

                        swal("Error!", "El aprendizaje no puede ser eliminado", "error");
                    }
                });
            });
    })

    $(document).on('click', 'a[id=editIndicador]', function (e) {

        e.preventDefault();

        $.getJSON("/RecursosCurriculares/AprendizajeEsperado/EditIndicador/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#aprendizajeId').val() + "/" + $(this).attr('data-value'), function (data) {

            if (data === "500") {

                swal("Error!", "Se ha producido un error al cargar la información", "error");
            }
            else {

                $('#indicadorId').val(data.IndicadorItem.Id)
                $('#tipoEducacionIndicadorCodigo').val(data.TipoEducacionNombre);
                $('#gradoIndicadorCodigo').val(data.GradoNombre);
                $('#sectorIndicadorId').val(data.SectorNombre);
                $('#aprendizajeIndicadorNumero').val(data.IndicadorItem.Numero);
                $('#aprendizajeIndicadorDescripcion').val(data.Descripcion);
                $('#aprendizajeIndicadorCategoria').val(data.IndicadorItem.CategoriaCodigo);
                $('#indicadorDescripcion').val(data.IndicadorItem.Descripcion);

                popUp();
            }
        });
    })

    $(document).on('click', 'a[id=deleteIndicador]', function (e) {

        e.preventDefault();

        var id = $(this).attr('data-value');

        swal({
            title: "¿Esta seguro?",
            text: "Se eliminará el indicador",
            type: "warning",
            showCancelButton: true,
            cancelButtonText: "Cancelar",
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Si, eliminalo",
            closeOnConfirm: false
        },
            function () {

                $.ajax({
                    type: 'GET',
                    url: '/RecursosCurriculares/AprendizajeEsperado/DeleteIndicador/' + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#aprendizajeId').val() + "/" + id,
                    success: function (data) {

                        if (data === "200") {

                            tableAprendizaje.ajax.reload();

                            tableIndicador.ajax.reload();

                            swal("Eliminado!", "El indicador fue eliminado de forma correcta", "success");
                        }
                        else {

                            swal("Error!", "El indicador no puede ser eliminado", "error");
                        }
                    },
                    error: function (data) {

                        swal("Error!", "El indicador no puede ser eliminado", "error");
                    }
                });
            });
    })

    var validAprendizaje = $('#formAprendizaje').validate({

        errorClass: 'state-error',
        validClass: 'state-success',
        errorElement: 'em',
        rules: {
            "Descripcion": {
                required: true
            }
        },
        messages: {
            "Descripcion": {
                required: 'Ingrese la descripción del aprendizaje'
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).closest('.field').addClass(errorClass).removeClass(validClass);
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).closest('.field').removeClass(errorClass).addClass(validClass);
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element.parent());
        },
        submitHandler: function (form) {

            var contenidos = [];
            var objetivosVerticales = [];

            var aprendizaje = {
                tipoEducacionCodigo: $('#tipoEducacion').val(),
                gradoCodigo: $('#grado').val(),
                sectorId: $('#sector').val(),
                id: $('#aprendizajeId').val(),
                numero: $('#aprendizajeNumero').val(),
                descripcion: $('#aprendizajeDescripcion').val()
            };

            $(contenidosId).each(function (i) {

                var ejeId = contenidosId[i].substring(0, 36);
                var contenidoId = contenidosId[i].substring(36, 72);

                var aprendizajeContenido = {
                    tipoEducacionCodigo: aprendizaje.tipoEducacionCodigo,
                    gradoCodigo: aprendizaje.gradoCodigo,
                    sectorId: aprendizaje.sectorId,
                    aprendizajeId: aprendizaje.id,
                    ejeId: ejeId,
                    contenidoId: contenidoId
                };

                contenidos.push(aprendizajeContenido);
            });

            $(objetivosVerticalesId).each(function (i) {

                var aprendizajeObjetivoVertical = {
                    tipoEducacionCodigo: aprendizaje.tipoEducacionCodigo,
                    gradoCodigo: aprendizaje.gradoCodigo,
                    sectorId: aprendizaje.sectorId,
                    aprendizajeId: aprendizaje.id,
                    objetivoVerticalId: objetivosVerticalesId[i]
                };

                objetivosVerticales.push(aprendizajeObjetivoVertical);
            });

            aprendizaje.contenidos = contenidos;
            aprendizaje.objetivosVerticales = objetivosVerticales;

            $.ajax({
                type: "POST",
                url: "/RecursosCurriculares/AprendizajeEsperado/AprendizajesEsperados",
                data: aprendizaje,
                success: function (data) {

                    if (data === "200") {

                        $('#indicadorForm').show();

                        tableAprendizaje.ajax.reload();

                        swal("Listo!", "Su información fue guardada correctamente", "success");
                    }
                    else {

                        swal("Error!", "Se ha producido un error al registrar la información", "error");
                    }
                },
                error: function (data) {

                    swal("Error!", "Se ha producido un error al registrar la información", "error");
                }
            });
        }
    })

    var validIndicador = $('#formModal').validate({

        errorClass: 'state-error',
        validClass: 'state-success',
        errorElement: 'em',
        rules: {
            "IndicadorItem.Descripcion": {
                required: true
            }
        },
        messages: {
            "IndicadorItem.Descripcion": {
                required: 'Ingrese la descripción del indicador'
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).closest('.field').addClass(errorClass).removeClass(validClass);
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).closest('.field').removeClass(errorClass).addClass(validClass);
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element.parent());
        },
        submitHandler: function (form) {

            var obj = {
                tipoEducacionCodigo: $('#tipoEducacion').val(),
                gradoCodigo: $('#grado').val(),
                sectorId: $('#sector').val(),
                aprendizajeId: $('#aprendizajeId').val(),
                id: $('#indicadorId').val(),
                categoriaCodigo: $('#aprendizajeIndicadorCategoria').val(),
                numero: $('#aprendizajeIndicadorNumero').val(),
                descripcion: $('#indicadorDescripcion').val()
            };

            $.ajax({
                type: "POST",
                url: "/RecursosCurriculares/AprendizajeEsperado/Indicadores",
                data: obj,
                success: function (data) {

                    $.magnificPopup.close();

                    if (data === "200") {

                        tableIndicador.ajax.reload();

                        swal("Listo!", "Su información fue guardada correctamente", "success");
                    }
                    else {

                        swal("Error!", "Se ha producido un error al registrar la información", "error");
                    }
                },
                error: function (data) {

                    swal("Error!", "Se ha producido un error al registrar la información", "error");
                }
            });
        }
    })

    $('#cancel').click(function (e) {

        e.preventDefault();

        $('#form').show(500);

        $('#aprendizajeForm').hide(500);

        $('#indicadorForm').hide(500);
    })

    $('#cancelPopUp').click(function (e) {

        e.preventDefault();

        $.magnificPopup.close();
    })
})

function gridViewAprendizaje() {

    var table = $('#gridViewAprendizaje').DataTable({
        "ajax": "/RecursosCurriculares/AprendizajeEsperado/GetAprendizajesEsperados/" + $('#tipoEducacion').val() + "/" + $('#grado').val() + "/" + $("#sector").val(),
        "columns": [
            { "data": "Numero" },
            { "data": "Descripcion" },
            { "data": "DetalleIndicadores" },
            { "data": "CMO" },
            { "data": "OFV" },
            { "data": "Accion" }
        ],
        "destroy": true,
        "order": [[0, "asc"]],
        "columnDefs": [
            {
                "targets": [0],
                "searchable": true,
                "sortable": true
            },
            {
                "targets": [1],
                "searchable": false,
                "sortable": false
            },
            {
                "targets": [2],
                "searchable": false,
                "sortable": false
            },
            {
                "targets": [3],
                "searchable": false,
                "sortable": false
            },
            {
                "targets": [4],
                "searchable": false,
                "sortable": false
            },
            {
                "targets": [5],
                "searchable": false,
                "sortable": false
            }
        ],
        "iDisplayLength": 5,
        "aLengthMenu": [
            [5, 10, 15, 20, 25, 30, -1],
            [5, 10, 15, 20, 25, 30, "All"]
        ],
        "fnInitComplete": function (oSettings, json) {

            if ($('#sector').val() !== "-1") {

                if (!$('#addAprendizaje').length) {

                    $("#gridViewAprendizaje_length").append('<br /><a class="btn btn-success btn-xs" id="addAprendizaje" href="#" title="Agregar aprendizaje" typebutton="Add"><i class="fa fa-plus"></i></a>');
                }

            }
        },
        "sDom": '<"dt-panelmenu clearfix"lfr>t<"dt-panelfooter clearfix"ip>'
    });

    return table;
}

function gridViewIndicador() {

    var table = $('#gridViewIndicador').DataTable({
        "ajax": "/RecursosCurriculares/AprendizajeEsperado/GetAprendizajesIndicadores/" + $('#tipoEducacion').val() + "/" + $('#grado').val() + "/" + $("#sector").val() + "/" + $("#aprendizajeId").val(),
        "columns": [
            { "data": "Numero" },
            { "data": "Descripcion" },
            { "data": "Habilidad" },
            { "data": "Accion" }
        ],
        "destroy": true,
        "order": [[0, "asc"]],
        "columnDefs": [
            {
                "targets": [0],
                "searchable": false,
                "sortable": true
            },
            {
                "targets": [1],
                "searchable": true,
                "sortable": true
            },
            {
                "targets": [2],
                "searchable": true,
                "sortable": true
            },
            {
                "targets": [3],
                "searchable": false,
                "sortable": false
            }
        ],
        "iDisplayLength": 5,
        "aLengthMenu": [
            [10, 15, 20, 25, 30, -1],
            [10, 15, 20, 25, 30, "All"]
        ],
        "fnInitComplete": function (oSettings, json) {

            if (!$('#addIndicador').length) {
                $("#gridViewIndicador_length").append('<br /><a class="btn btn-success btn-xs" id="addIndicador" href="#" title="Agregar indicador" typebutton="Add"><i class="fa fa-plus"></i></a>');
            }
        },
        "sDom": '<"dt-panelmenu clearfix"lfr>t<"dt-panelfooter clearfix"ip>'
    });

    return table;
}

function treeContenidos(contenidosId) {

    $("#contenidos").fancytree({
        extensions: ["childcounter"],
        source: $.ajax({
            type: "GET",
            url: "/RecursosCurriculares/AprendizajeEsperado/GetAprendizajeContenidos/" + $('#tipoEducacion').val() + '/' + $('#grado').val() + '/' + $('#sector').val() + '/' + $('#aprendizajeId').val(),
            success: function (data) {

                contenidosId.length = 0;

                $(data).each(function (i, eje) {

                    $(eje.children).each(function (j, contenido) {

                        if (contenido.selected) {

                            contenidosId.push(contenido.key);
                        }
                    });
                });
            },
            error: function (data) {

                swal("Error!", "Se ha producido un error al cargar los contenidos", "error");
            }
        }),
        selectMode: 3,
        checkbox: true, // Show checkboxes.
        clickFolderMode: 2, // 1:activate, 2:expand, 3:activate and expand, 4:activate (dblclick expands)
        select: function (event, data) {

            var contenidoId;
            var index;

            if (data.node.key === 'eje') {

                if (data.node.selected) {

                    $(data.node.children).each(function (i) {

                        contenidoId = data.node.children[i].key;

                        index = jQuery.inArray(contenidoId, contenidosId);

                        if (index === -1) {

                            contenidosId.push(contenidoId);
                        }
                    });
                }
                else {

                    $(data.node.children).each(function (i) {

                        contenidoId = data.node.children[i].key;

                        index = jQuery.inArray(contenidoId, contenidosId);

                        if (index !== -1) {

                            contenidosId.splice(index, 1);
                        }
                    });
                }
            }
            else {

                contenidoId = data.node.key;

                index = jQuery.inArray(contenidoId, contenidosId);

                if (data.node.selected) {

                    if (index === -1) {

                        contenidosId.push(contenidoId);
                    }
                } else {

                    contenidosId.splice(index, 1);
                }
            }
        },
        childcounter: {
            deep: true,
            hideZeros: true,
            hideExpanded: true
        }
    })
}

function treeObjetivosVerticales(objetivosVerticalesId) {

    $("#objetivosVerticales").fancytree({
        extensions: ["childcounter"],
        source: $.ajax({
            type: "GET",
            url: "/RecursosCurriculares/AprendizajeEsperado/GetAprendizajeObjetivosVerticales/" + $('#tipoEducacion').val() + '/' + $('#grado').val() + '/' + $('#sector').val() + '/' + $('#aprendizajeId').val(),
            success: function (data) {

                objetivosVerticalesId.length = 0;

                $(data).each(function (i, objetivoVertical) {

                    if (objetivoVertical.selected) {

                        objetivosVerticalesId.push(objetivoVertical.key);
                    }
                });
            },
            error: function (data) {

                swal("Error!", "Se ha producido un error al cargar los objetivos verticales", "error");
            }
        }),
        selectMode: 3,
        checkbox: true, // Show checkboxes.
        clickFolderMode: 2, // 1:activate, 2:expand, 3:activate and expand, 4:activate (dblclick expands)
        select: function (event, data) {

            var objetivoVerticalId = data.node.key;

            var index = jQuery.inArray(objetivoVerticalId, objetivosVerticalesId);

            if (data.node.selected) {

                if (index === -1) {

                    objetivosVerticalesId.push(objetivoVerticalId);
                }
            } else {

                objetivosVerticalesId.splice(index, 1);
            }
        },
        childcounter: {
            deep: true,
            hideZeros: true,
            hideExpanded: true
        }
    })
}

function popUp() {

    var form = $('#modal-form');

    form.find(".state-error").removeClass("state-error");
    form.find(".state-success").removeClass("state-success");
    form.find("em").remove();

    $.magnificPopup.open({
        removalDelay: 500,
        items: {
            src: "#modal-form"
        },
        callbacks: {
            beforeOpen: function (e) {

                this.st.mainClass = "mfp-flipInX";
            }
        },
        midClick: true
    });
}