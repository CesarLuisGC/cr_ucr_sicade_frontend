export class NavItem {
    constructor(
        public nombre: string = '',
        public icono: string = '',
        public ruta: string = '',
        public hijos: NavItem[] = [],
    ) { }
}