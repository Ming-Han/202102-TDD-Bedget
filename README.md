202102-TDD-Budget
===

## Test Cases

### Query one day budget in January

Given Budget

  | YearMonth | Amount |
  |-----------|--------|
  | 202101    | 31     |

When query from 2021/01/01 to 2021/01/01
Then return `1`

* [ ] Completed

### Query January budget

Given Budget

  | YearMonth | Amount |
  |-----------|--------|
  | 202101    | 62     |

When query from 2021/01/01 to 2021/01/31
Then return `62`

* [ ] Completed

### Query from January to February

Given Budget

  | YearMonth | Amount |
  |-----------|--------|
  | 202101    | 62     |
  | 202102    | 28     |

When query from 2021/01/01 to 2021/02/28
Then return `90`

* [ ] Completed

### Query from January 30 to February 1

Given Budget

  | YearMonth | Amount |
  |-----------|--------|
  | 202101    | 62     |
  | 202102    | 28     |

When query from 2021/01/30 to 2020/02/01
Then return `5`

* [ ] Completed

### Query from February 1 to January 1

Given Budget

  | YearMonth | Amount |
  |-----------|--------|
  | 202101    | 62     |
  | 202102    | 28     |

When query from 2021/02/01 to 2020/01/01
Then return `0`

* [ ] Completed
