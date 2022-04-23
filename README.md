# CNatural
Solo échale un ojo al método UpdateSale, en este es donde agregué los últimos detalles que me dijiste(como el uso de Include y tal), en los otros no tengo eso,
así que mejor dejarlos así. 
Si quieres que de un producto(Product) haya más cantidad solo tienes que actualizarlo y modificar su cantidad de manera manual, eso es algo que se debería hacer
con las inversiones, pero como no he implementado Quartz no le he entrado a eso.
Con respecto al método UpdateSale uno de los problemas está en que no se actualiza nada de la venta, pues yo paso como parámetro una(que puede tener entidades mal) 
y puedo pasarle null si quiero a algunas(ya vimos lo de hacer el DTO), pero con la que trabajo es con la que pido del context y esa es la que actualizo, a pesar de que
la que paso como parámetro tiene elementos que debería tomar la venta que pido de la base de datos(tiene elementos que no, como los que paso null o con valores
arbitrarios). La cantidad del producto en stock no es consistente porque como la venta que pido de la base de datos es la primera que agregué ya que no se le modifica el
Count cuando suma para devolver a Product.Count su valor inicial suma siempre el mismo número
