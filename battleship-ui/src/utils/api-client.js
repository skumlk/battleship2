// import { queryCache } from 'react-query'
// import * as auth from 'auth-provider'
const apiURL = "http://localhost:5000/api"

async function HttpClient(
    endpoint,
    { data, method, headers: customHeaders, ...customConfig } = {},
) {

    const token = localStorage.getItem("token")
    const config = {
        method: method ? method : data ? 'POST' : 'GET',
        body: data ? JSON.stringify(data) : undefined,
        headers: {
            Authorization: token ? `Bearer ${token}` : undefined,
            'Content-Type': data ? 'application/json' : undefined,
            ...customHeaders,
        },
        ...customConfig,
    }

    return window.fetch(`${apiURL}/${endpoint}`, config).then(async response => {
        // if (response.status === 401) {
        //     queryCache.clear()
        //     await auth.logout()
        //     window.location.assign(window.location)
        //     return Promise.reject({ message: 'Please re-authenticate.' })
        // }
        const data = await response.json()
        if (response.ok) {
            return data
        } else {
            return Promise.reject(data)
        }
    })
}

export { HttpClient }