import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {environment} from '../../environments/environment'
import { Page, PageRequest } from '../../app/models/pagination.model';

const BASE_API_URL = environment.apiUrl;

export abstract class ApiService {
constructor(
    protected  http: HttpClient,
    protected  url: string
) {}

    public get = (id: string): Observable<any> => this.http.get<any>(this.buildUrl(id));

    public query = (endpoint?:string): Observable<any> => this.http.get<any>(this.buildUrl(null, endpoint));

    public list (pageRequest?: PageRequest): Observable<Page<any>> {
        let params = new HttpParams ();
        if(pageRequest){
            params.set("pageNumber", pageRequest.pageNumber.toString());
            params.set("pageSize", pageRequest.pageSize.toString());
        }
          return this.http.get<Page<any>>(this.buildUrl(), { params: params });
    }

    public listDataTable = (dataTableParameters: any, endpoint?:string, additionalParameters?: any) : Observable<Page<any>> => 
        this.http.post<Page<any>>(this.buildUrl(null, endpoint), Object.assign(dataTableParameters, additionalParameters) , {});

    public post = (entity: any): Observable<any> => this.http.post(this.buildUrl(), entity);

    public put = (entity: any): Observable<any> => this.http.put(this.buildUrl(), entity);

    public delete = (id: string): Observable<any> => this.http.delete(this.buildUrl(id));

    protected buildUrl = (id?:string|null, endpoint?:string): string  => `${BASE_API_URL}${this.url}`+ (endpoint ? `/${endpoint}`: '') + (id ? `/${id}`: '');

}