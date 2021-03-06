﻿
        $(document).ready(function () {
            var dataTable = $('#tableReferences').DataTable({
                'order': [[1, "desc"]],
                'iDisplayLength': 25,
                'bFilter': false,
                'processing': true,
                'serverSide': true,
                'ajax': {
                    'url': '/Root/References/GetRefs',
                    'data': {
                    }
                },
                "language": {
                    //   "url": "http://cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Russian.json"
                    "url": "/Scripts/datatable/russian.json"


                },
                "columns": [
                        { "data": "Id", "autoWidth": true, },
                        { "data": "Name", "autoWidth": true },
                        { "data": "Alias", "autoWidth": true },
                        { "data": "Id", "autoWidth": true },
                ]
            });

            $('#tableReferences').on('draw.dt', function () {
                InitEditBtnForTable(dataTable);
            });

            function InitEditBtnForTable(dataTable) {
                $('#tableReferences tbody tr').each(function (i) {
                    var id = $(this).children('td:nth-child(1)').text();

                    var editBtn = '<a href="/Root/References/EditReferences/' + id + '"  class="btn btn-icon-only green">' +
                                    '<span class="fa fa-edit"></span>' +
                        '</a>';

                    var btnDel =
                        '<a href="/Root/References/DeleteReference/' + id + '"' + 'class="btn btn-icon-only red">' + 
                                        '<span class="fa fa-remove"></span>' +
                                '</a>'

                    $(this).children('td:nth-child(4)').html(editBtn+btnDel);
                });
                isGet = false;
            }

            function toDateFromJson(src) {
                return new Date(parseInt(src.substr(6)));
            }

        });
