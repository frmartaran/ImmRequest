export class StatusHelper {
    static getStatusNameFromNumber(statusId: number){
        switch (statusId) {
            case 0:
              return 'Created'
            case 1:
              return 'OnRevision'
            case 2:
              return 'Acepted'
            case 3:
              return 'Declined'
            case 4:
              return 'Ended'
            default:
              throw new Error('Your status doesnt exist')
        }  
    }
    static getStatusNumberFromName(statusName: string){
        switch (statusName) {
            case 'Created':
              return 0
            case 'OnRevision':
              return 1
            case 'Acepted':
              return 2
            case 'Declined':
              return 3
            case 'Ended':
              return 4
            default:
              throw new Error('Your status doesnt exist')
        }    
    }
}