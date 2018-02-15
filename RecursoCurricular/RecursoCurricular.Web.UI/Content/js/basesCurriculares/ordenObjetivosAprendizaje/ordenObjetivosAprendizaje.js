﻿jQuery(document).ready(function () {

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

    $(document).on('click', 'a[typebutton=Edit]', function (e) {

        e.preventDefault();

        $('#unidadId').val($(this).attr('data-value'));

        $('#form').hide(500);

        $('#divForm').show(500);

        $.ajax({
            url: "/BasesCurriculares/OrdenObjetivoAprendizaje/ObjetivosAprendizaje/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val() + '/' + $(this).attr('data-value'),
            method: "GET",
            processData: false,
            contentType: false,
        }).done(function (data) {

            $('#unidadObjetivoAprendizaje').html(data);

        });
    })

    $('#save').click(function (e) {

        e.preventDefault();

        var ordenes = [];

        $('.itemPadre').each(function (i, objetivo) {

            var orden = {

                objetivoAprendizajeId: $(objetivo.childNodes[1]).val(),
                indicadores:[]
            }

            $(objetivo.childNodes[2].childNodes[0].childNodes).each(function (j, indicador) {

                orden.indicadores.push($(indicador.childNodes[1]).val());

            });

            ordenes.push(orden);
        });

        var obj = {
            tipoEducacionCodigo: $('#tipoEducacion').val(),
            gradoCodigo: $('#grado').val(),
            sectorId: $('#sector').val(),
            unidadId: $('#unidadId').val(),
            ordenes: ordenes
        };

        $.ajax({
            type: "POST",
            url: "/BasesCurriculares/OrdenObjetivoAprendizaje/OrdenObjetivoAprendizaje",
            data: obj,
            success: function (data) {

                if (data === "200") {


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
    });
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
        "sDom": '<"dt-panelmenu clearfix"lfr>t<"dt-panelfooter clearfix"ip>'
    });

    return table;
}

//function treeHabilidades(subHabilidadesId) {

//    $("#habilidades").fancytree({
//        extensions: ["childcounter"],
//        source: $.ajax({
//            type: "GET",
//            url: "/BasesCurriculares/Unidad/GetHabilidades/" + $('#unidadId').val() + '/' + $('#tipoEducacion').val() + '/' + $('#grado').val() + '/' + $('#sector').val(),
//            success: function (data) {

//                subHabilidadesId.length = 0;

//                $(data).each(function (i, habilidad) {

//                    $(habilidad.children).each(function (j, subHabilidad) {

//                        if (subHabilidad.selected) {

//                            subHabilidadesId.push(subHabilidad.key);
//                        }
//                    });
//                });
//            },
//            error: function (data) {

//                swal("Error!", "Se ha producido un error al cargar las habilidades", "error");
//            }
//        }),
//        selectMode: 3,
//        checkbox: true, // Show checkboxes.
//        clickFolderMode: 2, // 1:activate, 2:expand, 3:activate and expand, 4:activate (dblclick expands)
//        select: function (event, data) {

//            var subHabilidadId;
//            var index;

//            if (data.node.key === 'padre') {

//                if (data.node.selected) {

//                    $(data.node.children).each(function (i) {

//                        subHabilidadId = data.node.children[i].key;

//                        index = jQuery.inArray(subHabilidadId, subHabilidadesId);

//                        if (index === -1) {

//                            subHabilidadesId.push(subHabilidadId);
//                        }
//                    });
//                }
//                else {

//                    $(data.node.children).each(function (i) {

//                        subHabilidadId = data.node.children[i].key;

//                        index = jQuery.inArray(subHabilidadId, subHabilidadesId);

//                        if (index !== -1) {

//                            subHabilidadesId.splice(index, 1);
//                        }
//                    });
//                }
//            }
//            else {

//                subHabilidadId = data.node.key;

//                index = jQuery.inArray(subHabilidadId, subHabilidadesId);

//                if (data.node.selected) {

//                    if (index === -1) {

//                        subHabilidadesId.push(subHabilidadId);
//                    }
//                } else {

//                    subHabilidadesId.splice(index, 1);
//                }
//            }
//        },
//        childcounter: {
//            deep: true,
//            hideZeros: true,
//            hideExpanded: true
//        }
//    })
//}

//function treeObjetivosAprendizaje(indicadoresId) {

//    $("#objetivosAprendizajes").fancytree({
//        extensions: ["childcounter"],
//        source: $.ajax({
//            type: "GET",
//            url: "/BasesCurriculares/Unidad/GetIndicadores/" + $('#unidadId').val() + '/' + $('#tipoEducacion').val() + '/' + $('#grado').val() + '/' + $('#sector').val(),
//            success: function (data) {

//                indicadoresId.length = 0;

//                $(data).each(function (i, eje) {

//                    $(eje.children).each(function (j, objetivoAprendizaje) {

//                        $(objetivoAprendizaje.children).each(function (k, indicador) {

//                            if (indicador.selected) {

//                                indicadoresId.push(indicador.key);
//                            }
//                        });
//                    });
//                });
//            },
//            error: function (data) {

//                swal("Error!", "Se ha producido un error al cargar los objtivos de aprendizaje", "error");
//            }
//        }),
//        selectMode: 3,
//        checkbox: true, // Show checkboxes.
//        clickFolderMode: 2, // 1:activate, 2:expand, 3:activate and expand, 4:activate (dblclick expands)
//        select: function (event, data) {

//            if (data.node.key === 'eje') {

//                if (data.node.selected) {

//                    $(data.node.children).each(function (i) {

//                        $(data.node.children[i].children).each(function (j) {

//                            indicadorId = data.node.children[i].children[j].key;

//                            index = jQuery.inArray(indicadorId, indicadoresId);

//                            if (index === -1) {

//                                indicadoresId.push(indicadorId);
//                            }
//                        });
//                    });
//                }
//                else {

//                    $(data.node.children).each(function (i) {

//                        $(data.node.children[i].children).each(function (j) {

//                            indicadorId = data.node.children[i].children[j].key;

//                            index = jQuery.inArray(indicadorId, indicadoresId);

//                            if (index !== -1) {

//                                indicadoresId.splice(index, 1);
//                            }
//                        });
//                    });
//                }
//            }
//            else if (data.node.key === 'objetivoAprendizaje') {

//                if (data.node.selected) {

//                    $(data.node.children).each(function (i) {

//                        indicadorId = data.node.children[i].key;

//                        index = jQuery.inArray(indicadorId, indicadoresId);

//                        if (index === -1) {

//                            indicadoresId.push(indicadorId);
//                        }
//                    });
//                }
//                else {

//                    $(data.node.children).each(function (i) {

//                        indicadorId = data.node.children[i].key;

//                        index = jQuery.inArray(indicadorId, indicadoresId);

//                        if (index !== -1) {

//                            indicadoresId.splice(index, 1);
//                        }
//                    });
//                }
//            }
//            else {

//                if (data.node.selected) {

//                    indicadorId = data.node.key;

//                    index = jQuery.inArray(indicadorId, indicadoresId);

//                    if (index === -1) {

//                        indicadoresId.push(indicadorId);
//                    }
//                }
//                else {

//                    indicadorId = data.node.key;

//                    index = jQuery.inArray(indicadorId, indicadoresId);

//                    if (index !== -1) {

//                        indicadoresId.splice(index, 1);
//                    }
//                }
//            }
//        },
//        childcounter: {
//            deep: true,
//            hideZeros: true,
//            hideExpanded: true
//        }
//    });
//}

//function treeActitudes(actitudesId) {

//    $("#actitudes").fancytree({
//        extensions: ["childcounter"],
//        source: $.ajax({
//            type: "GET",
//            url: "/BasesCurriculares/Unidad/GetActitudes/" + $('#unidadId').val() + '/' + $('#tipoEducacion').val() + '/' + $('#grado').val() + '/' + $('#sector').val(),
//            success: function (data) {

//                actitudesId.length = 0;

//                $(data).each(function (i, actitud) {

//                    if (actitud.selected) {

//                        actitudesId.push(actitud.key);
//                    }
//                });
//            },
//            error: function (data) {

//                swal("Error!", "Se ha producido un error al cargar las actitudes", "error");
//            }
//        }),
//        selectMode: 3,
//        checkbox: true, // Show checkboxes.
//        clickFolderMode: 2, // 1:activate, 2:expand, 3:activate and expand, 4:activate (dblclick expands)
//        select: function (event, data) {

//            var actitudId = data.node.key;

//            var index = jQuery.inArray(actitudId, actitudesId);

//            if (data.node.selected) {

//                if (index === -1) {

//                    actitudesId.push(actitudId);
//                }
//            } else {

//                actitudesId.splice(index, 1);
//            }
//        },
//        childcounter: {
//            deep: true,
//            hideZeros: true,
//            hideExpanded: true
//        }
//    })
//}

//function treeConocimientos(conocimientosId) {

//    $("#conocimientos").fancytree({
//        extensions: ["childcounter"],
//        source: $.ajax({
//            type: "GET",
//            url: "/BasesCurriculares/Unidad/GetConocimientos/" + $('#unidadId').val() + '/' + $('#tipoEducacion').val() + '/' + $('#grado').val() + '/' + $('#sector').val(),
//            success: function (data) {

//                conocimientosId.length = 0;

//                $(data).each(function (i, conocimiento) {

//                    if (conocimiento.selected) {

//                        conocimientosId.push(conocimiento.key);
//                    }
//                });
//            },
//            error: function (data) {

//                swal("Error!", "Se ha producido un error al cargar los conocimientos", "error");
//            }
//        }),
//        selectMode: 3,
//        checkbox: true, // Show checkboxes.
//        clickFolderMode: 2, // 1:activate, 2:expand, 3:activate and expand, 4:activate (dblclick expands)
//        select: function (event, data) {

//            var conocimientoId = data.node.key;

//            var index = jQuery.inArray(conocimientoId, conocimientosId);

//            if (data.node.selected) {

//                if (index === -1) {

//                    conocimientosId.push(conocimientoId);
//                }
//            } else {

//                conocimientosId.splice(index, 1);
//            }
//        },
//        childcounter: {
//            deep: true,
//            hideZeros: true,
//            hideExpanded: true
//        }
//    })
//}