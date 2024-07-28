import { Menu } from "./menu.model";
import { Usuario } from "./usuario.model";

export class Sesion {
    public token: string;
    public usuario: Usuario;
    public menus: Menu[];

    constructor() {
        this.token = "";
        this.usuario = new Usuario();
        this.menus = [];
    }
}