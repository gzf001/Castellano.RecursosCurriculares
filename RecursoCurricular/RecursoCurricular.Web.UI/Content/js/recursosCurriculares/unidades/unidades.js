jQuery(document).ready(function () {

    var aprendizajesId = [];

    var table;

    $('#divForm').hide();

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

        gridView();
    })

    $('#grado').change(function (e) {

        e.preventDefault();

        $("#sector").html("<option value='-1'>[Seleccione]</option>");
        $("#eje").html("<option value='-1'>[Seleccione]</option>");

        if ($('#grado').val() > 0) {

            $.getJSON('/Educacion/Home/Sectores/', function (data) {

                var items = "";

                $.each(data, function (i, sector) {
                    items += "<option value='" + sector.Value + "'>" + sector.Text + "</option>";
                });

                $("#sector").html(items);
            });
        }

        gridView();
    })

    $('#sector').change(function (e) {

        e.preventDefault();

        table = gridView();
    })

    $('#accordion').accordion({
        active: false,
        collapsible: true,
        heightStyle: "content"
    });

    $(document).on('click', 'a[typebutton=Add]', function (e) {

        e.preventDefault();

        $.getJSON("/RecursosCurriculares/Unidad/AddUnidad/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val(), function (data) {

            if (data === "500") {

                swal("Error!", "Seleccione el sector", "error");
            }
            else {

                $(":ui-fancytree").fancytree("destroy");

                $('#tipoEducacionCodigo').val(data.TipoEducacionNombre);
                $('#gradoCodigo').val(data.GradoNombre)
                $('#sectorId').val(data.SectorNombre);
                $('#unidadId').val(data.Id);
                $('#numero').val(data.Numero);
                $('#nombre').val(data.Nescripcion);

                treeAprendizajes(aprendizajesId);

                $('#form').hide(500);

                $('#divForm').show(500);
            }
        });
    })

    $(document).on('click', 'a[typebutton=Edit]', function (e) {

        e.preventDefault();

        $.getJSON("/RecursosCurriculares/Unidad/EditUnidad/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + '/' + $(this).attr('data-value'), function (data) {

            if (data === "500") {

                swal("Error!", "Seleccione el sector", "error");
            }
            else {

                $(":ui-fancytree").fancytree("destroy");

                $('#tipoEducacionCodigo').val(data.TipoEducacionNombre);
                $('#gradoCodigo').val(data.GradoNombre)
                $('#sectorId').val(data.SectorNombre);
                $('#unidadId').val(data.Id);
                $('#numero').val(data.Numero);
                $('#nombre').val(data.Nombre);

                treeAprendizajes(aprendizajesId);

                $('#form').hide(500);

                $('#divForm').show(500);
            }
        });
    })

    $(document).on('click', 'a[typebutton=Delete]', function () {

        var id = $(this).attr('data-value');

        swal({
            title: "¿Esta seguro?",
            text: "Se eliminará la unidad",
            type: "warning",
            showCancelButton: true,
            cancelButtonText: "Cancelar",
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Si, eliminala",
            closeOnConfirm: false
        },
            function () {

                $.ajax({
                    type: 'GET',
                    url: '/RecursosCurriculares/Unidad/DeleteUnidad/' + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + id,
                    success: function (data) {

                        if (data === "200") {

                            table.ajax.reload();

                            swal("Eliminado!", "La unidad fue eliminada de forma correcta", "success");
                        }
                        else {

                            swal("Error!", "La unidad no puede ser eliminada", "error");
                        }
                    },
                    error: function (data) {

                        swal("Error!", "La unidad no puede ser eliminada", "error");
                    }
                });
            });
    })

    var validator = $('#unidadForm').validate({

        errorClass: 'state-error',
        validClass: 'state-success',
        errorElement: 'em',
        rules: {
            Nombre: {
                required: true
            }
        },
        messages: {
            Nombre: {
                required: 'Ingrese el nombre de la unidad'
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

            var aprendizajes = [];

            var unidad = {
                tipoEducacionCodigo: $('#tipoEducacion').val(),
                gradoCodigo: $('#grado').val(),
                sectorId: $('#sector').val(),
                id: $('#unidadId').val(),
                numero: $('#numero').val(),
                nombre: $('#nombre').val()
            };

            $(aprendizajesId).each(function (i) {

                var aprendizajeId = aprendizajesId[i];

                var a = {
                    tipoEducacionCodigo: unidad.tipoEducacionCodigo,
                    gradoCodigo: unidad.gradoCodigo,
                    sectorId: unidad.sectorId,
                    id: aprendizajeId,
                    numero: 1,
                    descripcion: 'aprendizaje'
                };

                aprendizajes.push(a);
            });

            unidad.aprendizajes = aprendizajes;

            $.ajax({
                type: "POST",
                url: "/RecursosCurriculares/Unidad/Unidades",
                data: unidad,
                success: function (data) {

                    if (data === "200") {

                        table.ajax.reload();

                        $.magnificPopup.close();

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
})

$('#cancel').click(function (e) {

    e.preventDefault();

    $('#form').show(500);

    $('#divForm').hide(500);
})

function gridView() {

    var table = $('#gridView').DataTable({
        "ajax": "/RecursosCurriculares/Unidad/GetUnidades/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val(),
        "columns": [
            { "data": "Numero" },
            { "data": "Nombre" },
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
                "searchable": true,
                "sortable": true
            },
            {
                "targets": [2],
                "searchable": false,
                "sortable": false
            }
        ],
        "iDisplayLength": 15,
        "aLengthMenu": [
            [15, 20, 25, 30, -1],
            [15, 20, 25, 30, "All"]
        ],
        "fnInitComplete": function (oSettings, json) {

            if ($('#sector').val() !== "-1") {
                $("div.dataTables_length").append('<br /><a class="btn btn-success btn-xs" href="#" title="Agregar unidad" typebutton="Add"><i class="fa fa-plus"></i></a>');
            }
        },
        "sDom": '<"dt-panelmenu clearfix"lfr>t<"dt-panelfooter clearfix"ip>'
    });

    return table;
}

function treeAprendizajes(aprendizajesId) {

    $("#aprendizajes").fancytree({
        extensions: ["childcounter"],
        source: $.ajax({
            type: "GET",
            url: "/RecursosCurriculares/Unidad/GetAprendizajes/" + $('#unidadId').val() + '/' + $('#tipoEducacion').val() + '/' + $('#grado').val() + '/' + $('#sector').val(),
            success: function (data) {

                aprendizajesId.length = 0;

                $(data).each(function (i, aprendizaje) {

                    if (aprendizaje.selected) {

                        aprendizajesId.push(aprendizaje.key);
                    }
                });
            },
            error: function (data) {

                swal("Error!", "Se ha producido un error al cargar los aprendizajes", "error");
            }
        }),
        selectMode: 3,
        checkbox: true, // Show checkboxes.
        clickFolderMode: 2, // 1:activate, 2:expand, 3:activate and expand, 4:activate (dblclick expands)
        select: function (event, data) {

            var aprendizajeId = data.node.key;

            var index = jQuery.inArray(aprendizajeId, aprendizajesId);

            if (data.node.selected) {

                if (index === -1) {

                    aprendizajesId.push(aprendizajeId);
                }
            } else {

                aprendizajesId.splice(index, 1);
            }
        },
        childcounter: {
            deep: true,
            hideZeros: true,
            hideExpanded: true
        }
    })
}