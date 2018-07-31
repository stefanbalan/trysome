export class Expense {
    constructor(
        public amount: number = 0,
        public category: CategoryRef | undefined) { }
}

export class CategoryRef {
    constructor(
        public id: number,
        public name: string) { }
}
