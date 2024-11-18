window.onload = getUsuarios();

function getUsuarios() {
    $.ajax({
        // la URL para la petición
        url: '../../Admin/GetCuentas',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: {},
        // especifica si la petición es de tipo POST o GET
        type: 'GET',
        // el tipo de información que se espera de respuesta
        success: function (usuarios) {
            const tabla = document.getElementById('List-Usuarios');
            let contenidoTabla = ``;
            console.log(usuarios);
            $.each(usuarios, function (index, usuario) {
              contenidoTabla += `
              <tr class="fila-resaltar">
                  <td class="borde-td align-middle"><div><p>${usuario.nombre}</p></div></td>
                  <td class="borde-td align-middle"><div><p>${usuario.fechaNacimiento} <br>(${usuario.edad} Años)</p></div></td>
                  <td class="borde-td align-middle"><div><p>${usuario.email}</p></div></td>
                  <td class="borde-td align-middle" style="min-width: 5rem;"><div><p>${usuario.altura} m</p></div></td>
                  <td class="borde-td align-middle" style="min-width: 8rem;"><div><p>${usuario.peso} Kg</p></div></td>
                  <td class="borde-td align-middle"><div><p>${usuario.imc.slice(0, 5)}</p></div></td>
                  <td class="borde-td align-middle"><div><p>${usuario.sexo}</p></div></td>
                  <td class="borde-td align-middle"><div><p>${usuario.rol}</p></div></td>
                  
              </tr>`;
          });
            tabla.innerHTML = contenidoTabla;
        },
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al guardar el registro');
        }
    });
}

function InformeUsuarios() {
    // var doc = new jsPDF();
    var doc = new jsPDF("l", "mm", [297, 210]);
  
    var totalPagesExp = "{total_pages_count_string}";
    var pageContent = function (data) {
      var pageHeight =
        doc.internal.pageSize.height || doc.internal.pageSize.getHeight();
      var pageWidth =
        doc.internal.pageSize.width || doc.internal.pageSize.getWidth();
  
      // // HEADER
      // doc.setFontSize(14);
      // doc.setFont("helvetica", "bold");
      // doc.text("Informe de publicaciones por fecha", 15, 10);
  
      // FOOTER
      var str = "Pagina " + data.pageCount;
      // Total page number plugin only available in jspdf v1.0+
      if (typeof doc.putTotalPages == "function") {
        str = str + " de " + totalPagesExp;
      }
  
      doc.setLineWidth(8);
      doc.setDrawColor(238, 238, 238);
      doc.line(14, pageHeight - 11, 196, pageHeight - 11);
  
      doc.setFontSize(10);
  
      doc.setFontStyle("bold");
  
      doc.text(str, 17, pageHeight - 10);
    };
  
    var elem = document.getElementById("Tabla-Usuarios");
    var res = doc.autoTableHtmlToJson(elem);
    doc.autoTable(res.columns, res.data, {
      addPageContent: pageContent,
      theme: "grid",
      styles: { fillColor: [0, 143, 81], halign: "center" },
      columnStyles: 
        {
          0: { halign: "center", fillColor: [255, 255, 255], fontSize: 7 },
          1: { halign: "left", fillColor: [255, 255, 255], fontSize: 7 },
          2: { halign: "left", fillColor: [255, 255, 255], fontSize: 7 },
          3: { halign: "left", fillColor: [255, 255, 255], fontSize: 7 },
          4: { halign: "left", fillColor: [255, 255, 255], fontSize: 7, cellWidth: 30 },
          5: { halign: "right", fillColor: [255, 255, 255], fontSize: 7 },
          6: { halign: "left", fillColor: [255, 255, 255], fontSize: 7 },
          7: { halign: "left", fillColor: [255, 255, 255], fontSize: 7 },
        },
      margin: { top: 10 },
    });
  
    // ESTO SE LLAMA ANTES DE ABRIR EL PDF PARA QUE MUESTRE EN EL PDF EL NRO TOTAL DE PAGINAS. ACA CALCULA EL TOTAL DE PAGINAS.
    if (typeof doc.putTotalPages === "function") {
      doc.putTotalPages(totalPagesExp);
    }
  
    //doc.save('InformeSistema.pdf')
  
    var string = doc.output("datauristring");
    var iframe =
      "<iframe width='100%' height='100%' src='" + string + "'></iframe>";
  
    var x = window.open();
    x.document.open();
    x.document.write(iframe);
    x.document.close();
  }