
export class PageRequest {
  public pageNumber: number;
  public pageSize: number;
  public sort: Sort;

  constructor(page: number = 1, size: number = 10, sort: Sort = new Sort()) {
    this.pageNumber = page;
    this.pageSize = size;
    this.sort = sort;
  }

  public static from(page: number, size: number, sortColumn: string, sortDirection: string): PageRequest {
    const sort: Sort = Sort.from(sortColumn, sortDirection);
    return new PageRequest(page, size, sort);
  }
}

export class Page<T>  {    
    draw: number;
    recordsFiltered: number;
    recordsTotal: number;
    sort: Sort;
    search: Search;
    data: Array<T>;
  
    constructor(obj: any) {
      Object.assign(this, obj);
    }
  }

  export enum SortDirection {
    ASCENDING = 'ASC',
    DESCENDING = 'DESC'
  }


export class Sort {
  public direction: SortDirection;
  public column: string;

  constructor(column: string = 'id', direction: SortDirection = SortDirection.ASCENDING) {
    this.direction = direction; 
    this.column = column;
  }

  public static from(column: string, direction: string): Sort {
    switch (direction.toUpperCase()) {
      case 'ASC': return new Sort(column, SortDirection.ASCENDING);
      case 'DESC': return new Sort(column, SortDirection.DESCENDING);
      default: return new Sort(column, SortDirection.ASCENDING);
    }
  }
}

export class Search
{
    public Value: string;
    public Regex: boolean;
}