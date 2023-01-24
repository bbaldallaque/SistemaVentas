import { DetalleVenta } from "./detalle-venta";

export interface Venta {
    idVenta?: number,
    numeroDocumento?: string,
    tipoPago: string,
    fechaRegistro?: string,
    totalRexto: string,
    detalleVenta: DetalleVenta
}
