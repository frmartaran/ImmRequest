export class HtmlHelpers {
    static getHtmlErrorMessage(error: any){
        if(error.error && typeof error.error !== 'object'){
            return error.error        
        }else{
            return error.message
        }    
    }
}

