jQuery(document).ready(function () {

    var habilidadesId = [];
    var objetivosAprendizajeId = [];
    var actitudesId = [];
    var conocimientosId = [];

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
        collapsible: true,
        heightStyle: "content"
    });

    $("#treeHabilidades").fancytree({
        extensions: ["childcounter"],
        selectMode: 3,
        checkbox: true, // Show checkboxes.
        clickFolderMode: 2, // 1:activate, 2:expand, 3:activate and expand, 4:activate (dblclick expands)
        select: function (event, data) {

            var habilidadId;
            var index;

            if (data.node.key === 'padre') {

                if (data.node.selected) {

                    $(data.node.children).each(function (i) {

                        habilidadId = data.node.children[i].key;

                        index = jQuery.inArray(habilidadId, habilidadesId);

                        if (index === -1) {

                            habilidadesId.push(habilidadId);
                        }
                    });
                }
                else {

                    $(data.node.children).each(function (i) {

                        habilidadId = data.node.children[i].key;

                        index = jQuery.inArray(habilidadId, habilidadesId);

                        if (index !== -1) {

                            habilidadesId.splice(index, 1);
                        }
                    });
                }
            }
            else {

                habilidadId = data.node.key;

                index = jQuery.inArray(habilidadId, habilidadesId);

                if (data.node.selected) {

                    if (index === -1) {

                        habilidadesId.push(habilidadId);
                    }
                } else {

                    habilidadesId.splice(index, 1);
                }
            }
        },
        childcounter: {
            deep: true,
            hideZeros: true,
            hideExpanded: true
        }
    })

    $("#treeObjetivosAprendizajes").fancytree({
        extensions: ["childcounter"],
        selectMode: 3,
        checkbox: true, // Show checkboxes.
        clickFolderMode: 2, // 1:activate, 2:expand, 3:activate and expand, 4:activate (dblclick expands)
        select: function (event, data) {

            var objetivoAprendizajeId;
            var index;

            if (data.node.key === 'padre') {

                if (data.node.selected) {

                    $(data.node.children).each(function (i) {

                        objetivoAprendizajeId = data.node.children[i].key;

                        index = jQuery.inArray(objetivoAprendizajeId, objetivosAprendizajeId);

                        if (index === -1) {

                            objetivosAprendizajeId.push(objetivoAprendizajeId);
                        }
                    });
                }
                else {

                    $(data.node.children).each(function (i) {

                        objetivoAprendizajeId = data.node.children[i].key;

                        index = jQuery.inArray(objetivoAprendizajeId, objetivosAprendizajeId);

                        if (index !== -1) {

                            objetivosAprendizajeId.splice(index, 1);
                        }
                    });
                }
            }
            else {

                objetivoAprendizajeId = data.node.key;

                index = jQuery.inArray(objetivoAprendizajeId, objetivosAprendizajeId);

                if (data.node.selected) {

                    if (index === -1) {

                        objetivosAprendizajeId.push(objetivoAprendizajeId);
                    }
                } else {

                    objetivosAprendizajeId.splice(index, 1);
                }
            }
        },
        childcounter: {
            deep: true,
            hideZeros: true,
            hideExpanded: true
        }
    })

    $("#treeActitudes").fancytree({
        extensions: ["childcounter"],
        selectMode: 3,
        checkbox: true, // Show checkboxes.
        clickFolderMode: 2, // 1:activate, 2:expand, 3:activate and expand, 4:activate (dblclick expands)
        select: function (event, data) {

            var actitudId = data.node.key;

            var index = jQuery.inArray(actitudId, actitudesId);

            if (data.node.selected) {

                if (index === -1) {

                    actitudesId.push(actitudId);
                }
            } else {

                actitudesId.splice(index, 1);
            }
        },
        childcounter: {
            deep: true,
            hideZeros: true,
            hideExpanded: true
        }
    })

    $("#treeConocimientos").fancytree({
        extensions: ["childcounter"],
        selectMode: 3,
        checkbox: true, // Show checkboxes.
        clickFolderMode: 2, // 1:activate, 2:expand, 3:activate and expand, 4:activate (dblclick expands)
        select: function (event, data) {

            var conocimientoId = data.node.key;

            var index = jQuery.inArray(conocimientoId, conocimientosId);

            if (data.node.selected) {

                if (index === -1) {

                    conocimientosId.push(conocimientoId);
                }
            } else {

                conocimientosId.splice(index, 1);
            }
        },
        childcounter: {
            deep: true,
            hideZeros: true,
            hideExpanded: true
        }
    })

    $(document).on('click', 'a[typebutton=Add]', function (e) {

        e.preventDefault();

        $.getJSON("/BasesCurriculares/Unidad/AddUnidad/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val(), function (data) {

            if (data === "500") {

                swal("Error!", "Seleccione el sector", "error");
            }
            else {

                $('#tipoEducacionCodigo').val(data.TipoEducacionNombre);
                $('#gradoCodigo').val(data.GradoNombre)
                $('#sectorId').val(data.SectorNombre);
                $('#unidadId').val(data.Id);
                $('#proposito').val(data.Proposito);
                $('#conocimientoPrevio').val(data.Descripcion);
                $('#palabraClave').val(data.Descripcion);
                $('#numero').val(data.Numero);
                $('#nombre').val(data.Nescripcion);

                $('#form').hide(500);

                $('#divForm').show(500);
            }
        })
    })

    $(document).on('click', 'a[typebutton=Edit]', function () {

        $.getJSON("/BasesCurriculares/ObjetivoAprendizaje/EditObjetivoAprendizaje/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#eje').val() + "/" + $(this).attr('data-value'), function (data) {

            if (data === "500") {

                swal("Error!", "Se ha producido un error al cargar la información", "error");
            }
            else {

                $('#tipoEducacionCodigo').val(data.TipoEducacion.Nombre);
                $('#gradoCodigo').val(data.Grado.Nombre)
                $('#sectorId').val(data.Sector.Nombre);
                $('#ejeId').val(data.Eje.Nombre);
                $('#objetivoAprendizajeId').val(data.Id);
                $('#numero').val(data.Numero);
                $('#descripcion').val(data.Descripcion);

                popUp();
            }
        })
    })

    $(document).on('click', 'a[typebutton=Delete]', function () {

        var id = $(this).attr('data-value');

        swal({
            title: "¿Esta seguro?",
            text: "Se eliminará el objetivo de aprendizaje",
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
                    url: '/BasesCurriculares/ObjetivoAprendizaje/DeleteObjetivoAprendizaje/' + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + "/" + $('#eje').val() + "/" + id,
                    success: function (data) {

                        if (data === "200") {

                            table.ajax.reload();

                            swal("Eliminado!", "El objetivo de aprendizaje fue eliminado de forma correcta", "success");
                        }
                        else {

                            swal("Error!", "El objetivo de aprendizaje no puede ser eliminado", "error");
                        }
                    },
                    error: function (data) {

                        swal("Error!", "El objetivo de aprendizaje no puede ser eliminado", "error");
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

            alert('aqui');

            //var obj = {
            //    tipoEducacionCodigo: $('#tipoEducacion').val(),
            //    gradoCodigo: $('#grado').val(),
            //    sectorId: $('#sector').val(),
            //    ejeId: $('#eje').val(),
            //    id: $('#objetivoAprendizajeId').val(),
            //    numero: $('#numero').val(),
            //    descripcion: $('#descripcion').val()
            //};

            //$.ajax({
            //    type: "POST",
            //    url: "/BasesCurriculares/ObjetivoAprendizaje/ObjetivosAprendizaje",
            //    data: obj,
            //    success: function (data) {

            //        if (data === "200") {

            //            table.ajax.reload();

            //            $.magnificPopup.close();

            //            swal("Listo!", "Su información fue guardada correctamente", "success");
            //        }
            //        else {

            //            swal("Error!", "Se ha producido un error al registrar la información", "error");
            //        }
            //    },
            //    error: function (data) {

            //        swal("Error!", "Se ha producido un error al registrar la información", "error");
            //    }
            //});
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
        "ajax": "/BasesCurriculares/Unidad/GetUnidades/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val(),
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
                $("div.dataTables_length").append('<br /><a class="btn btn-success btn-xs" href="#" title="Agregar objetivo de aprendizaje" typebutton="Add"><i class="fa fa-plus"></i></a>');
            }
        },
        "sDom": '<"dt-panelmenu clearfix"lfr>t<"dt-panelfooter clearfix"ip>'
    });

    return table;
}