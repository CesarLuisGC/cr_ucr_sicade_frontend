export class Usuario {
    constructor(
        public usuario_Id: number = -1,
        public nombre: string = "",
        public cedula: string = "",
        public sexo: number = -1,
        public correo: string = "",
        public contrasenna: string = "",
        public rol_Id: number = -1,
        public puesto: string = "",
        public claseOcupacional: string = "",
        public dependencia: string = "",
        public telefono: string = "",
        public custom1: string = "",
        public custom2: string = "",
        public custom3: string = "",
        public custom4: string = "",
        public fechaRegistro: string = "1900-01-01",
        public fechaModificación: string = "1900-01-01",
        public activo: boolean = false
    ) {}
}
