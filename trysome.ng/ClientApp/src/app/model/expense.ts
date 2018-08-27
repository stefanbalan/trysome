export class Expense {
  constructor(
    public date: Date,
    public amount: number,
    public category: string,
    public tag: string
  ) {}
}
