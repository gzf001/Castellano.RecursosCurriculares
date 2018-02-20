jQuery(document).ready(function () {

    var contenidosId = [];
    var objetivosVerticalesId = [];
    var indicadores = [];

    var table;

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

        table = gridViewAprendizaje();
    })

    $('#accordion').accordion({
        collapsible: true,
        heightStyle: "content"
    });

    $(document).on('click', 'a[id=addAprendizaje]', function (e) {

        e.preventDefault();

        $.getJSON("/RecursosCurriculares/AprendizajeEsperado/AddAprendizaje/" + $("#tipoEducacion").val() + "/" + $('#grado').val() + "/" + $('#sector').val(), function (data) {

            if (data === "500") {

                swal("Error!", "Seleccione el objetivo de aprendizaje", "error");
            }
            else {

                $(":ui-fancytree").fancytree("destroy");

                $('#aprendizajeId').val(data.Id);
                $('#aprendizajeTipoEducacion').val(data.TipoEducacionNombre);
                $('#aprendizajeGrado').val(data.GradoNombre);
                $('#aprendizajeSector').val(data.SectorNombre);
                $('#aprendizajeNumero').val(data.Numero);
                $('#aprendizajeDescripcion').val(data.Descripcion);

                gridViewIndicador();

                treeContenidos(contenidosId);

                treeObjetivosVerticales(objetivosVerticalesId);

                $('#form').hide(500);

                $('#indicadorForm').show(500);
            }
        })
    })

    $(document).on('click', 'a[id=addIndicador]', function (e) {

        e.preventDefault();

        $('#tipoEducacionIndicadorCodigo').val($('#aprendizajeTipoEducacion').val());
        $('#gradoIndicadorCodigo').val($('#aprendizajeGrado').val());
        $('#sectorIndicadorId').val($('#aprendizajeSector').val());
        $('#aprendizajeIndicadorDescripcion').val($('#aprendizajeDescripcion').val());

        popUp();
    })

    var validator = $('#formModal').validate({

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
                required: 'Ingrese la descripción'
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

            var indicador = {
                tipoEducacionCodigo: $('#tipoEducacion').val(),
                gradoCodigo: $('#grado').val(),
                sectorId: $('#sector').val(),
                aprendizajeId: $('#aprendizajeId').val(),
                id: $('#indicadorId').val(),
                categoriaCodigo: $('#aprendizajeIndicadorCategorias').val(),
                numero: $('#aprendizajeIndicadorNumero').val(),
                descripcion: $('#indicadorDescripcion').val()
            };

            index = jQuery.inArray(indicador, indicadores);

            if (index === -1) {

                indicadores.push(indicador);
            }
            else {

                indicadores[index] = indicador;
            }

            var z = $("#gridViewIndicador tbody tr").length;

            if ($("#gridViewIndicador tbody tr").length === 1) {

                var dataSet = [[indicador.numero, indicador.descripcion, $("#aprendizajeIndicadorCategorias option:selected").text(), "<a class='btn btn-primary btn-xs btn-flat actionLinkCrudEmbedded tooltipstered' title='Ediatr' index='0' typebutton='Edit'><i class='fa fa-times'></i></a><a class='btn btn-danger btn-xs actionLinkCrudEmbedded' title='Eliminar' index='0' typebutton='Delete'><i class='fa fa-times'></i></a>"]];

                table = $('#gridViewIndicador').DataTable({
                    "data": dataSet,
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

                        $("#gridViewIndicador_length").append('<br /><a class="btn btn-success btn-xs" id="addIndicador" href="#" title="Agregar indicador" typebutton="Add"><i class="fa fa-plus"></i></a>');
                    },
                    "sDom": '<"dt-panelmenu clearfix"lfr>t<"dt-panelfooter clearfix"ip>'
                });
            }
            
            $.magnificPopup.close();
            //var dataset = [];

            swal("Listo!", "El indicador fue guardado correctamente", "success");
        }
    })

    $('#cancel').click(function (e) {

        e.preventDefault();

        $('#form').show(500);

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
            { "data": "CMO" },
            { "data": "OFV" },
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
        "iDisplayLength": 15,
        "aLengthMenu": [
            [15, 20, 25, 30, -1],
            [15, 20, 25, 30, "All"]
        ],
        "fnInitComplete": function (oSettings, json) {

            if ($('#sector').val() !== "-1") {
                $("div.dataTables_length").append('<br /><a class="btn btn-success btn-xs" id="addAprendizaje" href="#" title="Agregar objetivo vertical" typebutton="Add"><i class="fa fa-plus"></i></a>');
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
            { "data": "Categoria" },
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

            $("#gridViewIndicador_length").append('<br /><a class="btn btn-success btn-xs" id="addIndicador" href="#" title="Agregar indicador" typebutton="Add"><i class="fa fa-plus"></i></a>');
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

            if (data.node.key === 'padre') {

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