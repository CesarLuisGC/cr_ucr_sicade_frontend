import { NavMenu } from "./navMenu.model";
import { Usuario } from "./usuario.model";

export class Sesion {
    public token: string;
    public usuario: Usuario;
    public menus: NavMenu[];

    constructor() {
        this.token = "";
        this.usuario = new Usuario();
        this.menus = [];
    }
}