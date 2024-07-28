export class Menu {
    constructor(
        public menu_Id: number = -1,
        public padre_Id: number = -1,
        public nombre: string = "",
        public icono: string = "",
        public ruta: string = "",
        public nivel: number = -1,
        public activo: boolean = false
    ) {}
}