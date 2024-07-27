export class ApiResponse{
    constructor(
        public data: object,
        public message: string,
        public state?: number
    ){}
}