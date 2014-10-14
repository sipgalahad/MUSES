<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MPStudentPageTrx.master" AutoEventWireup="true" 
    CodeBehind="StudentSubjectChartInformation.aspx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Program.StudentSubjectChartInformation" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <link rel="stylesheet" type="text/css" href='<%= ResolveUrl("~/Libs/Scripts/jquery/jqplot/jquery.jqplot.min.css")%>' /> 
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/jquery/jqplot/jquery.jqplot.min.js")%>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/jquery/jqplot/plugins/jqplot.highlighter.min.js")%>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/jquery/jqplot/plugins/jqplot.blockRenderer.min.js")%>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/jquery/jqplot/plugins/jqplot.enhancedLegendRenderer.min.js")%>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/jquery/jqplot/plugins/jqplot.pointLabels.min.js")%>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/jquery/jqplot/plugins/jqplot.cursor.min.js")%>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/jquery/jqplot/plugins/jqplot.canvasTextRenderer.min.js")%>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/jquery/jqplot/plugins/jqplot.canvasAxisTickRenderer.min.js")%>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/jquery/jqplot/plugins/jqplot.categoryAxisRenderer.min.js")%>'></script>

    <script type="text/javascript">
        $(document).ready(function () {
            var line1 = [[['X-1 / 1', 90], ['X-1 / 2', 92], ['XI-A1 / 1', 89], ['XI-A1 / 2', 92]],
                         [['X-1 / 1', 93], ['X-1 / 2', 95], ['XI-A1 / 1', 94], ['XI-A1 / 2', 95]],
                         [['X-1 / 1', 85], ['X-1 / 2', 83], ['XI-A1 / 1', 84], ['XI-A1 / 2', 82]],
                         [['X-1 / 1', 88], ['X-1 / 2', 90], ['XI-A1 / 1', 89], ['XI-A1 / 2', 89]],
                         [['X-1 / 1', 81], ['X-1 / 2', 85], ['XI-A1 / 1', 80], ['XI-A1 / 2', 82]],
                         [['X-1 / 1', 87], ['X-1 / 2', 84], ['XI-A1 / 1', 83], ['XI-A1 / 2', 89]]];
            function getLabels() {
                var arr = new Array(4);
                arr[0] = { label: 'Matematika', showLabel: true, lineWidth: 2 };
                arr[1] = { label: 'Fisika', showLabel: true, lineWidth: 2 };
                arr[2] = { label: 'Kimia', showLabel: true, lineWidth: 2 };
                arr[3] = { label: 'Biologi', showLabel: true, lineWidth: 2 };
                arr[4] = { label: 'Bahasa Indonesia', showLabel: true, lineWidth: 2 };
                arr[5] = { label: 'Bahasa Inggris', showLabel: true, lineWidth: 2 };
                return arr;
            };

            var plot1 = $.jqplot('chart', line1, {
                series: [{ showMarker: false}],
                title: 'Grafik Rapor',
                animate: true,
                cursor: {
                    show: true,
                    zoom: true
                },
                axesDefaults: {
                    tickRenderer: $.jqplot.CanvasAxisTickRenderer,
                    tickOptions: {
                        fontSize: '10pt'
                    }
                },
                highlighter: {
                    show: true,
                    showLabel: true,
                    tooltipAxes: 'y',
                    sizeAdjust: 7.5, tooltipLocation: 'ne'
                },
                series: getLabels(),
                legend: {
                    renderer: $.jqplot.EnhancedLegendRenderer,
                    show: true
                },
                seriesDefaults: {
                    showMarker: false,
                    rendererOptions: {
                        showDataLabels: true
                    }
                },
                axes: {
                    xaxis: {
                        label: 'Kelas / Semester',
                        renderer: $.jqplot.CategoryAxisRenderer,
                        tickOptions: {
                            //angle: -45,
                            fontSize: '10pt'
                        }
                    },
                    yaxis: {
                        label: 'Nilai',
                        min: 50,
                        max: 100,
                        tickInterval: 5
                    }
                }
            });
        });

    </script>

    <div class="example-content">
        <div id="chart" style="width:800px;height:500px"></div>
    </div>
</asp:Content>