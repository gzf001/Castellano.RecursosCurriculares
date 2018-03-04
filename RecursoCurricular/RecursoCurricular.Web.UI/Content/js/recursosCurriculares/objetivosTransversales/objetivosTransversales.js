jQuery(document).ready(function () {

    var tableObjetivo;
    var tableIndicador;

    $('#objetivoForm').hide();

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

        gridViewObjetivo();
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

        gridViewObjetivo();
    })

    $('#sector').change(function (e) {

        e.preventDefault();

        $("#unidad").html("<option value='-1'>[Seleccione]</option>");

        if ($('#sector').val() !== '-1') {

            var tipoEducacion = $('#tipoEducacion').val();
            var grado = $('#grado').val();
            var sector = $(this).val();

            $.getJSON('/RecursosCurriculares/Home/Unidades/' + tipoEducacion + "/" + grado + "/" + sector, function (data) {

                var items = "";

                $.each(data, function (i, unidad) {
                    items += "<option value='" + unidad.Value + "'>" + unidad.Text + "</option>";
                });

                $("#unidad").html(items);
            });
        }

        gridViewObjetivo();
    })

    $('#unidad').change(function (e) {

        e.preventDefault();

        tableObjetivo = gridViewObjetivo();
    })

    $(document).on('click', 'a[id=addObjetivo]', function (e) {

        e.preventDefault();

        $.getJSON("/RecursosCurriculares/ObjetivoTransversal/AddObjetivoTransversal/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#unidad').val(), function (data) {

            if (data === "500") {

                swal("Error!", "Seleccione la unidad", "error");
            }
            else {

                $('#objetivoId').val(data.Id);
                $('#objetivoTipoEducacion').val(data.TipoEducacionNombre);
                $('#objetivoGrado').val(data.GradoNombre);
                $('#objetivoSector').val(data.SectorNombre);
                $('#objetivoUnidad').val(data.UnidadNombre);
                $('#objetivoNumero').val(data.Numero);
                $('#objetivoDescripcion').val(data.Descripcion);

                tableIndicador = gridViewIndicador();

                $('#indicadorForm').hide();

                $('#form').hide(500);

                $('#objetivoForm').show(500);
            }
        })
    })

    $(document).on('click', 'a[id=addIndicador]', function (e) {

        e.preventDefault();

        $.getJSON("/RecursosCurriculares/ObjetivoTransversal/AddObjetivoTransversalIndicador/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#unidad').val() + "/" + $('#objetivoId').val(), function (data) {

            if (data === "500") {

                swal("Error!", "Existen problemas al agregar un nuevo indicador, por favor refresque el navegador y reintente", "error");
            }
            else {

                $('#indicadorId').val(data.IndicadorItem.Id)
                $('#tipoEducacionIndicadorCodigo').val(data.TipoEducacionNombre);
                $('#gradoIndicadorCodigo').val(data.GradoNombre);
                $('#sectorIndicadorId').val(data.SectorNombre);
                $('#unidadIndicadorId').val(data.UnidadNombre);
                $('#objetivoIndicadorNumero').val(data.IndicadorItem.Numero);
                $('#objetivoIndicadorDescripcion').val(data.Descripcion);
                $('#indicadorDescripcion').val(data.IndicadorItem.Descripcion);

                popUp();
            }
        });
    })

    $(document).on('click', 'a[id=editObjetivo]', function (e) {

        e.preventDefault();

        $.getJSON("/RecursosCurriculares/ObjetivoTransversal/EditObjetivoTransversal/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#unidad').val() + "/" + $(this).attr('data-value'), function (data) {

            if (data === "500") {

                swal("Error!", "Existen problemas al editar el objetivo, por favor refresque el navegador y reintente", "error");
            }
            else {

                $('#objetivoId').val(data.Id);
                $('#objetivoTipoEducacion').val(data.TipoEducacionNombre);
                $('#objetivoGrado').val(data.GradoNombre);
                $('#objetivoSector').val(data.SectorNombre);
                $('#objetivoUnidad').val(data.UnidadNombre);
                $('#objetivoNumero').val(data.Numero);
                $('#objetivoDescripcion').val(data.Descripcion);

                tableIndicador = gridViewIndicador();

                $('#indicadorForm').show(500);

                $('#form').hide(500);

                $('#objetivoForm').show(500);
            }
        });
    })

    $(document).on('click', 'a[id=deleteObjetivo]', function (e) {

        e.preventDefault();

        var id = $(this).attr('data-value');

        swal({
            title: "¿Esta seguro?",
            text: "Se eliminará el objetivo transversal",
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
                    url: '/RecursosCurriculares/ObjetivoTransversal/DeleteObjetivoTransversal/' + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#unidad').val() + "/" + id,
                    success: function (data) {

                        if (data === "200") {

                            tableObjetivo.ajax.reload();

                            swal("Eliminado!", "El objetivo transversal fue eliminado de forma correcta", "success");
                        }
                        else {

                            swal("Error!", "El objetivo transversal no puede ser eliminado", "error");
                        }
                    },
                    error: function (data) {

                        swal("Error!", "El objetivo transversal no puede ser eliminado", "error");
                    }
                });
            });
    })

    $(document).on('click', 'a[id=editIndicador]', function (e) {

        e.preventDefault();

        $.getJSON("/RecursosCurriculares/ObjetivoTransversal/EditObjetivoTransversalIndicador/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#unidad').val() + "/" + $('#objetivoId').val() + "/" + $(this).attr('data-value'), function (data) {

            if (data === "500") {

                swal("Error!", "Se ha producido un error al cargar la información", "error");
            }
            else {

                $('#indicadorId').val(data.IndicadorItem.Id)
                $('#tipoEducacionIndicadorCodigo').val(data.TipoEducacionNombre);
                $('#gradoIndicadorCodigo').val(data.GradoNombre);
                $('#sectorIndicadorId').val(data.SectorNombre);
                $('#unidadIndicadorId').val(data.UnidadNombre);
                $('#objetivoIndicadorNumero').val(data.IndicadorItem.Numero);
                $('#objetivoIndicadorDescripcion').val(data.Descripcion);
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
                    url: '/RecursosCurriculares/ObjetivoTransversal/DeleteObjetivoTransversalIndicador/' + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#unidad').val() + "/" + $('#objetivoId').val() + "/" + id,
                    success: function (data) {

                        if (data === "200") {

                            tableObjetivo.ajax.reload();

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

    var validObjetivo = $('#formObjetivo').validate({

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
                required: 'Ingrese la descripción del objetivo transversal'
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

            var objetivo = {
                tipoEducacionCodigo: $('#tipoEducacion').val(),
                gradoCodigo: $('#grado').val(),
                sectorId: $('#sector').val(),
                unidadId: $('#unidad').val(),
                id: $('#objetivoId').val(),
                numero: $('#objetivoNumero').val(),
                descripcion: $('#objetivoDescripcion').val()
            };

            $.ajax({
                type: "POST",
                url: "/RecursosCurriculares/ObjetivoTransversal/ObjetivoTransversales",
                data: objetivo,
                success: function (data) {

                    if (data === "200") {

                        $('#indicadorForm').show();

                        tableObjetivo.ajax.reload();

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
                unidadId: $('#unidad').val(),
                objetivoTransversalId: $('#objetivoId').val(),
                id: $('#indicadorId').val(),
                numero: $('#objetivoIndicadorNumero').val(),
                descripcion: $('#indicadorDescripcion').val()
            };

            $.ajax({
                type: "POST",
                url: "/RecursosCurriculares/ObjetivoTransversal/ObjetivoTransversalIndicador",
                data: obj,
                success: function (data) {

                    $.magnificPopup.close();

                    if (data === "200") {

                        tableObjetivo.ajax.reload();

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

        $('#objetivoForm').hide(500);

        $('#indicadorForm').hide(500);
    })

    $('#cancelPopUp').click(function (e) {

        e.preventDefault();

        $.magnificPopup.close();
    })
})

function gridViewObjetivo() {

    var table = $('#gridViewObjetivo').DataTable({
        "ajax": "/RecursosCurriculares/ObjetivoTransversal/GetObjetivoTransversales/" + $('#tipoEducacion').val() + "/" + $('#grado').val() + "/" + $("#sector").val() + "/" + $("#unidad").val(),
        "columns": [
            { "data": "Numero" },
            { "data": "Descripcion" },
            { "data": "DetalleIndicadores" },
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
            }
        ],
        "iDisplayLength": 5,
        "aLengthMenu": [
            [5, 10, 15, 20, 25, 30, -1],
            [5, 10, 15, 20, 25, 30, "All"]
        ],
        "fnInitComplete": function (oSettings, json) {

            if ($('#unidad').val() !== "-1") {

                if (!$('#addObjetivo').length) {

                    $("#gridViewObjetivo_length").append('<br /><a class="btn btn-success btn-xs" id="addObjetivo" href="#" title="Agregar objetivo transversal" typebutton="Add"><i class="fa fa-plus"></i></a>');
                }

            }
        },
        "sDom": '<"dt-panelmenu clearfix"lfr>t<"dt-panelfooter clearfix"ip>'
    });

    return table;
}

function gridViewIndicador() {

    var table = $('#gridViewIndicador').DataTable({
        "ajax": "/RecursosCurriculares/ObjetivoTransversal/GetObjetivoTransversalIndicadores/" + $('#tipoEducacion').val() + "/" + $('#grado').val() + "/" + $("#sector").val() + "/" + $("#unidad").val() + "/" + $("#objetivoId").val(),
        "columns": [
            { "data": "Numero" },
            { "data": "Descripcion" },
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
            }
        ],
        "iDisplayLength": 5,
        "aLengthMenu": [
            [5, 10, 15, 20, 25, 30, -1],
            [5, 10, 15, 20, 25, 30, "All"]
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