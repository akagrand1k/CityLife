﻿
        $(document).ready(function () {
            var dataTable = $('#tableSysReferences').DataTable({
                'order': [[1, "desc"]],
                'iDisplayLength': 25,
                'bFilter': false,
                'processing': true,
                'serverSide': true,
                'ajax': {
                    'url': '/Administrator/Reference/GetSysRefs',
                    'data': {
                    }
                },
                "language": {
                    //   "url": "http://cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Russian.json"
                    "url": "/Scripts/datatable/russian.json"


                },
                "columns": [
                        { "data": "Id", "autoWidth": true, },
                        { "data": "Key", "autoWidth": true },
                        { "data": "Value", "autoWidth": true },
                        { "data": "Id", "autoWidth": true },
                ]
            });

            $('#tableSysReferences').on('draw.dt', function () {
                InitEditBtnForTable(dataTable);
            });

            function InitEditBtnForTable(dataTable) {
                $('#tableSysReferences tbody tr').each(function (i) {
                    var id = $(this).children('td:nth-child(1)').text();
                    var editBtn = '<a href="/Administrator/Reference/EditSysReferences/' + id + '"  class="btn btn-icon-only green">' +
                                    '<span class="fa fa-edit"></span>' +
                               '</a>';

                    $(this).children('td:nth-child(4)').html(editBtn);
                });
                isGet = false;
            }

            function toDateFromJson(src) {
                return new Date(parseInt(src.substr(6)));
            }

        });
