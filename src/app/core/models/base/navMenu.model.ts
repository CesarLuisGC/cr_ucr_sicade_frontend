import { NavItem } from "./navItem.model";

export class NavMenu {
    constructor(
        public titulo: string = '',
        public hijos: NavItem[] = []
    ) {}
}